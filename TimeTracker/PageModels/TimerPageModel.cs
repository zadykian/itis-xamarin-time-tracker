using System;
using System.Threading.Tasks;
using System.Timers;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.Notifications;
using TimeTracker.Services.Photo;
using TimeTracker.Services.TimeTracking;
using TimeTracker.Services.UserLocation;
using TimeTracker.ViewModels;

namespace TimeTracker.PageModels
{
	/// <summary>
	/// Timer page model.
	/// </summary>
	internal class TimerPageModel : PageModelBase
	{
		private readonly Timer generalTimer;
		private readonly Timer notificationTimer;

		private readonly ViewAllPageModel viewAllPageModel;

		private readonly ITrackedPeriodService trackedPeriodService;
		private readonly IAccountService accountService;
		private readonly ILocationService locationService;
		private readonly IPhotoService photoService;
		private readonly INotificationService notificationService;

		public TimerPageModel(
			ITrackedPeriodService trackedPeriodService,
			IAccountService accountService,
			ILocationService locationService,
			IPhotoService photoService,
			INotificationService notificationService,
			ViewAllPageModel viewAllPageModel)
		{
			this.trackedPeriodService = trackedPeriodService;
			this.accountService = accountService;
			this.locationService = locationService;
			this.photoService = photoService;
			this.notificationService = notificationService;

			this.viewAllPageModel = viewAllPageModel;

			TimerButtonViewModel = new ButtonViewModel("start timer", OnTimerButtonPressed);
			AttachPhotoButtonViewModel = new ButtonViewModel("attach photo", OnAttachPhotoButtonClicked, isEnabled: false);

			generalTimer = new Timer {Interval = 1000};
			generalTimer.Elapsed += (sender, args) => RunningTotal += TimeSpan.FromSeconds(1);

			notificationTimer = new Timer {Interval = TimeSpan.FromMinutes(1).TotalMilliseconds};
			notificationTimer.Elapsed += async (sender, args) => await OnNotificationTimerElapsed();
		}

		private bool timerIsStarted;

		public bool TimerIsStarted
		{
			get => timerIsStarted;
			set => SetProperty(ref timerIsStarted, value);
		}

		private TimeSpan runningTotal;

		public TimeSpan RunningTotal
		{
			get => runningTotal;
			set => SetProperty(ref runningTotal, value);
		}

		private DateTime currentStartTime;

		public DateTime CurrentStartTime
		{
			get => currentStartTime;
			set => SetProperty(ref currentStartTime, value);
		}

		private ButtonViewModel timerButtonViewModel;

		public ButtonViewModel TimerButtonViewModel
		{
			get => timerButtonViewModel;
			set => SetProperty(ref timerButtonViewModel, value);
		}

		private ButtonViewModel attachPhotoButtonViewModel;

		public ButtonViewModel AttachPhotoButtonViewModel
		{
			get => attachPhotoButtonViewModel;
			set => SetProperty(ref attachPhotoButtonViewModel, value);
		}

		private async void OnTimerButtonPressed()
		{
			if (TimerIsStarted) await OnTimerStopped();
			else await OnTimerStarted();
			TimerIsStarted = !TimerIsStarted;
		}

		private async Task OnTimerStarted()
		{
			CurrentStartTime = DateTime.Now;
			generalTimer.Start();
			notificationTimer.Start();
			TimerButtonViewModel.Text = "stop timer";
			AttachPhotoButtonViewModel.IsEnabled = true;

			var currentLocation = await locationService.GetCurrentLocationAsync();
			var newTrackedPeriod = new TrackedPeriod(accountService.CurrentUser.Id, currentLocation);
			await trackedPeriodService.UpsertAsync(newTrackedPeriod);
			viewAllPageModel.AllForCurrentUser.Insert(0, newTrackedPeriod);
		}

		private async Task OnTimerStopped()
		{
			generalTimer.Stop();
			notificationTimer.Stop();
			RunningTotal = TimeSpan.Zero;
			TimerButtonViewModel.Text = "start timer";
			AttachPhotoButtonViewModel.IsEnabled = false;

			var currentPeriod = await trackedPeriodService.GetCurrentAsync(accountService.CurrentUser.Id);
			currentPeriod.End = DateTime.Now;
			await trackedPeriodService.UpsertAsync(currentPeriod);
			viewAllPageModel.AllForCurrentUser.RemoveAt(0);
			viewAllPageModel.AllForCurrentUser.Insert(0, currentPeriod);
		}

		private async Task OnNotificationTimerElapsed()
		{
			var currentPeriod = await trackedPeriodService.GetCurrentAsync(accountService.CurrentUser.Id);
			await notificationService.PushNotificationAsync(currentPeriod.Total);
		}

		private async void OnAttachPhotoButtonClicked()
		{
			var imageContent = await photoService.CapturePhotoAsync();
			var currentTrackedPeriod = await trackedPeriodService.GetCurrentAsync(accountService.CurrentUser.Id);
			var image = new Image(imageContent, currentTrackedPeriod.Id);
			await trackedPeriodService.AddImageAsync(image);
		}
	}
}