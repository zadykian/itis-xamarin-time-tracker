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
		private readonly List<TrackedPeriod> trackedPeriods = new List<TrackedPeriod>();

		/// <inheritdoc />
		async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
		{
			// todo
			await Task.CompletedTask;
			trackedPeriods.RemoveAll(period => period.Id == trackedPeriod.Id);
			trackedPeriods.Add(trackedPeriod);
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(Guid userId)
		{
			// todo
			await Task.CompletedTask;
			return trackedPeriods.OrderBy(period => period.Start).ToArray();
		}
	}
}