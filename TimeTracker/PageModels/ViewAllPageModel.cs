using System.Collections.Generic;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.PageModels
{
	internal class ViewAllPageModel : PageModelBase
	{
		private readonly ITrackedPeriodService trackedPeriodService;
		private readonly IAccountService accountService;

		public ViewAllPageModel(
			ITrackedPeriodService trackedPeriodService,
			IAccountService accountService)
		{
			this.trackedPeriodService = trackedPeriodService;
			this.accountService = accountService;
		}
	}
}