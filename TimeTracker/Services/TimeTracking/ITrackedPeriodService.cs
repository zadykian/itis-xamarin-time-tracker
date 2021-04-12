using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.TimeTracking
{
	/// <summary>
	/// Service for <see cref="TrackedPeriod"/> entities management.
	/// </summary>
	internal interface ITrackedPeriodService
	{
		/// <summary>
		/// Add new or update existing tracked period.
		/// </summary>
		Task UpsertAsync(TrackedPeriod trackedPeriod);

		/// <summary>
		/// Get current tracked period for user with id <paramref name="userId"/>.
		/// </summary>
		Task<TrackedPeriod> GetCurrentAsync(int userId);

		/// <summary>
		/// Get all tracked periods for user with id <paramref name="userId"/>. 
		/// </summary>
		Task<IReadOnlyCollection<TrackedPeriod>> GetAllAsync(int userId);

		/// <summary>
		/// Delete data of user with id <paramref name="userId"/>.
		/// </summary>
		Task ClearDataAsync(int userId);

		/// <summary>
		/// Add new image related to current tracked period. 
		/// </summary>
		Task AddImageAsync(Image image);
	}
}