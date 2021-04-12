using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.TimeTracking
{
	/// <inheritdoc />
	internal class TrackedPeriodService  : ITrackedPeriodService
	{
		private readonly ICollection<TrackedPeriod> trackedPeriods = new List<TrackedPeriod>();

		/// <inheritdoc />
		async Task ITrackedPeriodService.AddAsync(TrackedPeriod trackedPeriod)
		{
			// todo
			await Task.CompletedTask;
			trackedPeriods.Add(trackedPeriod);
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(Guid userId)
		{
			// todo
			await Task.CompletedTask;
			return trackedPeriods.ToArray();
		}
	}
}