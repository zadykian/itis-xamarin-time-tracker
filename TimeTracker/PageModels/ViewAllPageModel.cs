using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
			AllForCurrentUser = new ObservableCollection<TrackedPeriod>();
		}

		/// <summary>
		/// All current user's tracked periods.
		/// </summary>
		public ObservableCollection<TrackedPeriod> AllForCurrentUser { get; }

		/// <inheritdoc />
		public override async Task InitializeAsync(object navigationData)
		{
			var currentUser = accountService.CurrentUser;
			var trackedPeriods = await trackedPeriodService.GetAllAsync(currentUser.Id);
			foreach (var trackedPeriod in trackedPeriods) AllForCurrentUser.Insert(0, trackedPeriod);
		}
	}
}