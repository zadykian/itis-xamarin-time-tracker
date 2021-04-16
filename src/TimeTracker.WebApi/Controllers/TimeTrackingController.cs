using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Services.Models;
using TimeTracker.Services.TimeTracking;
using TimeTracker.WebApi.Controllers.Base;

namespace TimeTracker.WebApi.Controllers
{
	/// <summary>
	/// Controller for <see cref="TrackedPeriod"/> entities management.
	/// </summary>
	public class TimeTrackingController : ApiControllerBase
	{
		private readonly ITrackedPeriodService trackedPeriodService;

		public TimeTrackingController(ITrackedPeriodService trackedPeriodService)
			=> this.trackedPeriodService = trackedPeriodService;

		/// <inheritdoc cref="ITrackedPeriodService.UpsertAsync"/>
		[HttpPut]
		public async Task<IActionResult> UpsertAsync([FromBody, Required] TrackedPeriod trackedPeriod)
		{
			await trackedPeriodService.UpsertAsync(trackedPeriod);
			return Ok();
		}

		/// <inheritdoc cref="ITrackedPeriodService.GetCurrentAsync"/>
		[HttpGet]
		public async Task<IActionResult> GetCurrentAsync([FromQuery, Required] int? userId)
		{
			var currentPeriod = await trackedPeriodService.GetCurrentAsync(userId!.Value);
			if (currentPeriod is null) return BadRequest();
			return Ok(currentPeriod);
		}

		/// <inheritdoc cref="ITrackedPeriodService.GetAllAsync"/>
		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery, Required] int? userId)
		{
			var allPeriods = await trackedPeriodService.GetAllAsync(userId!.Value);
			return Ok(allPeriods);
		}

		/// <inheritdoc cref="ITrackedPeriodService.ClearDataAsync"/>
		[HttpDelete]
		public async Task<IActionResult> ClearDataAsync([FromQuery, Required] int? userId)
		{
			await trackedPeriodService.ClearDataAsync(userId!.Value);
			return Ok();
		}
	}
}