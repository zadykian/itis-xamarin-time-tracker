using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Services.ConnectionFactory;

namespace TimeTracker.Services.TimeTracking
{
	/// <inheritdoc />
	internal class TrackedPeriodService :  ITrackedPeriodService
	{
		private readonly ITrackedPeriodService sqliteSubService;
		private readonly ITrackedPeriodService webApiSubService;

		public TrackedPeriodService()
		{
			sqliteSubService = new SqliteSubService();
			webApiSubService = new WebApiSubService();
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
		{
			await sqliteSubService.UpsertAsync(trackedPeriod);
			await webApiSubService.UpsertAsync(trackedPeriod);
		}

		/// <inheritdoc />
		async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
		{
			var currentLocal = await sqliteSubService.GetCurrentAsync(userId);

			if (currentLocal != null)
			{
				return currentLocal;
			}

			var currentRemote = await webApiSubService.GetCurrentAsync(userId);

			if (currentRemote is null)
			{
				throw new ArgumentException($"User with id {userId} does not have current active period.", nameof(userId));
			}

			await sqliteSubService.UpsertAsync(currentRemote);
			return currentRemote;
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
		{
			var allLocal = await sqliteSubService.GetAllAsync(userId);

			if (allLocal.Any())
			{
				return allLocal;
			}

			var allRemote = await webApiSubService.GetAllAsync(userId);
			foreach (var remotePeriod in allRemote) await sqliteSubService.UpsertAsync(remotePeriod);
			return allRemote;
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.ClearDataAsync(int userId)
		{
			await sqliteSubService.ClearDataAsync(userId);
			await webApiSubService.ClearDataAsync(userId);
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.AddImageAsync(Image image)
		{
			await sqliteSubService.AddImageAsync(image);
			await webApiSubService.AddImageAsync(image);
		}

		/// <summary>
		/// Tracked periods sub-service which is responsible for communication with SQLite database. 
		/// </summary>
		private class SqliteSubService : SqliteServiceBase, ITrackedPeriodService
		{
			/// <inheritdoc />
			async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
			{
				var dbConnection = await Connection.Value;
				await dbConnection.DeleteAsync(trackedPeriod);
				await dbConnection.InsertAsync(trackedPeriod);
			}

			/// <inheritdoc />
			async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
				=> await (await Connection.Value)
					.Table<TrackedPeriod>()
					.FirstOrDefaultAsync(period => period.UserId == userId && period.End == null);

			/// <inheritdoc />
			async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
				=> await (await Connection.Value)
					.Table<TrackedPeriod>()
					.Where(period => period.UserId == userId)
					.OrderByDescending(period => period.Start)
					.ToArrayAsync();

			/// <inheritdoc />
			async Task ITrackedPeriodService.ClearDataAsync(int userId)
				=> await (await Connection.Value)
					.Table<TrackedPeriod>()
					.DeleteAsync(period => period.UserId == userId && period.End != null);

			/// <inheritdoc />
			async Task ITrackedPeriodService.AddImageAsync(Image image)
				=> await (await Connection.Value).InsertAsync(image);
		}

		/// <summary>
		/// Tracked periods sub-service which is responsible for communication with remote Web API.
		/// </summary>
		private class WebApiSubService : ITrackedPeriodService
		{
			// todo: implement interaction with web api via http client

			/// <inheritdoc />
			async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
				=> await Task.CompletedTask;

			/// <inheritdoc />
			async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
			{
				await Task.CompletedTask;
				return null;
			}

			/// <inheritdoc />
			async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
			{
				await Task.CompletedTask;
				return ArraySegment<TrackedPeriod>.Empty;
			}

			/// <inheritdoc />
			async Task ITrackedPeriodService.ClearDataAsync(int userId) => await Task.CompletedTask;

			/// <inheritdoc />
			async Task ITrackedPeriodService.AddImageAsync(Image image) => await Task.CompletedTask;
		}
	}
}