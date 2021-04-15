using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.App.Core.Services.Http;
using TimeTracker.App.Core.Services.Http.Configuration;
using TimeTracker.Services.Models;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.App.Core.Services.TimeTracking
{
	/// <summary>
	/// Tracked periods service which is responsible for communication with remote Web API.
	/// </summary>
	internal class WebApiTrackedPeriodService : HttpServiceBase, ITrackedPeriodService
	{
		public WebApiTrackedPeriodService(IHttpConfiguration httpConfiguration) : base(httpConfiguration)
		{
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.UpsertAsync(TrackedPeriod trackedPeriod)
			=> await CallAsync(HttpMethod.Put, "TimeTracking", "Upsert", trackedPeriod);

		/// <inheritdoc />
		async Task<TrackedPeriod> ITrackedPeriodService.GetCurrentAsync(int userId)
		{
			var httpResponse = await CallAsync(HttpMethod.Get, "TimeTracking", "GetCurrent", $"{nameof(userId)}={userId}");
			return await httpResponse.FromBody<TrackedPeriod>();
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<TrackedPeriod>> ITrackedPeriodService.GetAllAsync(int userId)
		{
			var httpResponse = await CallAsync(HttpMethod.Get, "TimeTracking", "GetAll", $"{nameof(userId)}={userId}");
			return await httpResponse.FromBody<IReadOnlyCollection<TrackedPeriod>>() ?? ArraySegment<TrackedPeriod>.Empty;
		}

		/// <inheritdoc />
		async Task ITrackedPeriodService.ClearDataAsync(int userId)
			=> await CallAsync(HttpMethod.Delete, "TimeTracking", "ClearData", $"{nameof(userId)}={userId}");
	}
}