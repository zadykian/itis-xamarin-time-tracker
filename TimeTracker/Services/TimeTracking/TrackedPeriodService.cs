using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Models;
using TimeTracker.Services.ConnectionFactory;

namespace TimeTracker.Services.TimeTracking
{
	/// <inheritdoc />
	internal class TrackedPeriodService :  ITrackedPeriodService
	{
		private readonly ITrackedPeriodService sqliteSubService;

		public TrackedPeriodService()
		{
			sqliteSubService = new SqliteSubService();
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
		{
			// todo
			await sqliteSubService.UpsertAsync(trackedPeriod);
		}

		/// <inheritdoc />
		async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
		{
			// todo
			return await sqliteSubService.GetCurrentAsync(userId);
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
		{
			// todo
			return await sqliteSubService.GetAllAsync(userId);
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.ClearDataAsync(int userId)
		{
			// todo
			await sqliteSubService.ClearDataAsync(userId);
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.AddImageAsync(Image image)
		{
			// todo
			await sqliteSubService.AddImageAsync(image);
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
					.FirstAsync(period => period.UserId == userId && period.End == null);

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
	}
}