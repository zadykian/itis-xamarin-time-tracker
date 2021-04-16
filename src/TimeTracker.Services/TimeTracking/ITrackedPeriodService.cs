using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.TimeTracking
{
	/// <summary>
	/// Service for <see cref="TrackedPeriod"/> entities management.
	/// </summary>
	public interface ITrackedPeriodService
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
		/// Delete tracked periods of a user with id <paramref name="userId"/>.
		/// </summary>
		/// <remarks>
		/// If user has not finished tracked period, it stays untouched.
		/// </remarks>
		Task ClearDataAsync(int userId);
	}
}