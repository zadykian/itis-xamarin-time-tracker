using System;
using System.Threading.Tasks;
using System.Timers;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.TimeTracking;
using TimeTracker.ViewModels;

namespace TimeTracker.PageModels
{
	internal class TimerPageModel : PageModelBase
	{
		private readonly Timer timer;
		private readonly ITrackedPeriodService trackedPeriodService;
		private readonly IAccountService accountService;

		public TimerPageModel(
			ITrackedPeriodService trackedPeriodService,
			IAccountService accountService)
		{
			this.trackedPeriodService = trackedPeriodService;
			this.accountService = accountService;

			TimerButtonViewModel = new ButtonViewModel("start timer", OnTimerButtonClicked);
			AttachPhotoButtonViewModel = new ButtonViewModel("attach photo", OnAttachPhotoButtonClicked, isEnabled: false);

			timer = new Timer {Interval = 1000, Enabled = false};
			timer.Elapsed += OnTimerElapsed;
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

		public override async Task InitializeAsync(object navigationData)
		{
			RunningTotal = new TimeSpan();
			await base.InitializeAsync(navigationData);
		}

		private void OnTimerElapsed(object sender, ElapsedEventArgs e) => RunningTotal += TimeSpan.FromSeconds(1);

		private async void OnTimerButtonClicked()
		{
			if (TimerIsStarted)
			{
				timer.Enabled = false;
				RunningTotal = TimeSpan.Zero;
				TimerButtonViewModel.Text = "start timer";
				AttachPhotoButtonViewModel.IsEnabled = false;
				var newTrackedPeriod = new TrackedPeriod(accountService.CurrentUser.Id);
				await trackedPeriodService.UpsertAsync(newTrackedPeriod);
			}
			else
			{
				CurrentStartTime = DateTime.Now;
				timer.Enabled = true;
				TimerButtonViewModel.Text = "stop timer";
				AttachPhotoButtonViewModel.IsEnabled = true;
			}

			TimerIsStarted = !TimerIsStarted;
		}

		private async void OnAttachPhotoButtonClicked()
		{
			await Task.CompletedTask;
		}
	}
}