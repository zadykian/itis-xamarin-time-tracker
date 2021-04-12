using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TimeTracker.Models;

namespace TimeTracker.Services.TimeTracking
{
	/// <inheritdoc />
	internal class TrackedPeriodService  : ITrackedPeriodService
	{
		private readonly ITrackedPeriodService sqliteSubService;

		public TrackedPeriodService(SQLiteAsyncConnection dbConnection)
		{
			sqliteSubService = new SqliteSubService(dbConnection);
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
		private class SqliteSubService : ITrackedPeriodService
		{
			private readonly SQLiteAsyncConnection dbConnection;

			public SqliteSubService(SQLiteAsyncConnection dbConnection)
				=> this.dbConnection = dbConnection;

			/// <inheritdoc />
			async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
			{
				await dbConnection.DeleteAsync(trackedPeriod);
				await dbConnection.InsertAsync(trackedPeriod);
			}

			/// <inheritdoc />
			async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
				=> await dbConnection
					.Table<TrackedPeriod>()
					.FirstAsync(period => period.UserId == userId && period.End == null);

			/// <inheritdoc />
			async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
				=> await dbConnection
					.Table<TrackedPeriod>()
					.Where(period => period.UserId == userId)
					.OrderByDescending(period => period.Start)
					.ToArrayAsync();

			/// <inheritdoc />
			async Task ITrackedPeriodService.ClearDataAsync(int userId)
				=> await dbConnection
					.Table<TrackedPeriod>()
					.DeleteAsync(period => period.UserId == userId && period.End != null);

			/// <inheritdoc />
			async Task ITrackedPeriodService.AddImageAsync(Image image)
				=> await dbConnection.InsertAsync(image);
		}
	}
}