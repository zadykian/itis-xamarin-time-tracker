using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.App.Core.Services.Http;
using TimeTracker.Services.Models;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.App.Core.Services.TimeTracking
{
	/// <summary>
	/// Tracked periods service which is responsible for communication with remote Web API.
	/// </summary>
	internal class WebApiTrackedPeriodService : HttpServiceBase, ITrackedPeriodService
	{
		public WebApiTrackedPeriodService(HttpClient httpClient) : base(httpClient)
		{
		}

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
	}
}