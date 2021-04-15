using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Services.Models;
using TimeTracker.Services.TimeTracking;

// ReSharper disable SuggestBaseTypeForParameter

namespace TimeTracker.App.Core.Services.TimeTracking
{
	/// <inheritdoc />
	internal class TrackedPeriodService :  ITrackedPeriodService
	{
		private readonly ITrackedPeriodService sqliteTrackedPeriodService;
		private readonly ITrackedPeriodService webApiTrackedPeriodService;

		public TrackedPeriodService(
			SqliteTrackedPeriodService sqliteTrackedPeriodService,
			WebApiTrackedPeriodService webApiTrackedPeriodService)
		{
			this.sqliteTrackedPeriodService = sqliteTrackedPeriodService;
			this.webApiTrackedPeriodService = webApiTrackedPeriodService;
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
		{
			await sqliteTrackedPeriodService.UpsertAsync(trackedPeriod);
			await webApiTrackedPeriodService.UpsertAsync(trackedPeriod);
		}

		/// <inheritdoc />
		async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
		{
			var currentLocal = await sqliteTrackedPeriodService.GetCurrentAsync(userId);

			if (currentLocal != null)
			{
				return currentLocal;
			}

			var currentRemote = await webApiTrackedPeriodService.GetCurrentAsync(userId);

			if (currentRemote is null)
			{
				throw new ArgumentException($"User with id {userId} does not have current active period.", nameof(userId));
			}

			await sqliteTrackedPeriodService.UpsertAsync(currentRemote);
			return currentRemote;
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
		{
			var localPeriods = await sqliteTrackedPeriodService.GetAllAsync(userId);

			if (localPeriods.Any())
			{
				return localPeriods;
			}

			var remotePeriods = await webApiTrackedPeriodService.GetAllAsync(userId);
			foreach (var remotePeriod in remotePeriods) await sqliteTrackedPeriodService.UpsertAsync(remotePeriod);
			return remotePeriods;
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.ClearDataAsync(int userId)
		{
			await sqliteTrackedPeriodService.ClearDataAsync(userId);
			await webApiTrackedPeriodService.ClearDataAsync(userId);
		}
	}
}