using System;
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
		/// Get all tracked periods for user with id <paramref name="userId"/>. 
		/// </summary>
		Task<IReadOnlyCollection<TrackedPeriod>> GetAllAsync(Guid userId);
	}
}