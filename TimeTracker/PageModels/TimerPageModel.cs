using System;
using System.Threading.Tasks;
using System.Timers;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Work;
using TimeTracker.ViewModels;

namespace TimeTracker.PageModels
{
	internal class TimerPageModel : PageModelBase
	{
		private readonly Timer timer;
		private readonly IWorkService workService;

		public TimerPageModel(IWorkService workService)
		{
			this.workService = workService;

			TimerButtonModel = new ButtonModel("start timer", OnTimerButtonClicked);
			AttachPhotoButtonModel = new ButtonModel("attach photo", OnAttachPhotoButtonClicked, isEnabled: false);

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

		private ButtonModel timerButtonModel;

		public ButtonModel TimerButtonModel
		{
			get => timerButtonModel;
			set => SetProperty(ref timerButtonModel, value);
		}

		private ButtonModel attachPhotoButtonModel;

		public ButtonModel AttachPhotoButtonModel
		{
			get => attachPhotoButtonModel;
			set => SetProperty(ref attachPhotoButtonModel, value);
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
				TimerButtonModel.Text = "start timer";
				AttachPhotoButtonModel.IsEnabled = false;
				var item = new WorkItem {Start = CurrentStartTime, End = DateTime.Now};
				await workService.LogWorkAsync(item);
			}
			else
			{
				CurrentStartTime = DateTime.Now;
				timer.Enabled = true;
				TimerButtonModel.Text = "stop timer";
				AttachPhotoButtonModel.IsEnabled = true;
			}

			TimerIsStarted = !TimerIsStarted;
		}

		private async void OnAttachPhotoButtonClicked()
		{
			await Task.CompletedTask;
		}
	}
}