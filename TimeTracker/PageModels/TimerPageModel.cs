using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Work;
using TimeTracker.ViewModels.Buttons;

namespace TimeTracker.PageModels
{
	internal class TimerPageModel : PageModelBase
	{
		bool timerIsStarted;

		public bool TimerIsStarted
		{
			get => timerIsStarted;
			set => SetProperty(ref timerIsStarted, value);
		}

		TimeSpan _runningTotal;

		public TimeSpan RunningTotal
		{
			get => _runningTotal;
			set => SetProperty(ref _runningTotal, value);
		}

		DateTime _currentStartTime;

		public DateTime CurrentStartTime
		{
			get => _currentStartTime;
			set => SetProperty(ref _currentStartTime, value);
		}

		public ObservableCollection<WorkItem> WorkItems
		{
			get => _workItems;
			set => SetProperty(ref _workItems, value);
		}

		ButtonModel timerButtonModel;

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

		private readonly Timer _timer;
		private ObservableCollection<WorkItem> _workItems;
		private readonly IWorkService _workService;

		public TimerPageModel(IWorkService workService)
		{
			_workService = workService;

			TimerButtonModel = new ButtonModel("start timer", OnTimerButtonClicked);
			AttachPhotoButtonModel = new ButtonModel("attach photo", OnAttachPhotoButtonClicked, isEnabled: false);

			_timer = new Timer {Interval = 1000, Enabled = false};
			_timer.Elapsed += OnTimerElapsed;
		}

		private void OnTimerElapsed(object sender, ElapsedEventArgs e) => RunningTotal += TimeSpan.FromSeconds(1);

		public override async Task InitializeAsync(object navigationData)
		{
			RunningTotal = new TimeSpan();
			WorkItems = await _workService.GetTodaysWorkAsync();

			await base.InitializeAsync(navigationData);
		}

		private async void OnTimerButtonClicked()
		{
			if (TimerIsStarted)
			{
				_timer.Enabled = false;
				RunningTotal = TimeSpan.Zero;
				TimerButtonModel.Text = "start timer";
				AttachPhotoButtonModel.IsEnabled = false;
				var item = new WorkItem
				{
					Start = CurrentStartTime,
					End = DateTime.Now
				};
				WorkItems.Insert(0, item);
				await _workService.LogWorkAsync(item);
			}
			else
			{
				CurrentStartTime = DateTime.Now;
				_timer.Enabled = true;
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