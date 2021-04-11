using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.Work;
using TimeTracker.ViewModels.Buttons;

namespace TimeTracker.PageModels
{
	internal class TimeClockPageModel : PageModelBase
	{
		bool _isClockedIn;

		public bool IsClockedIn
		{
			get => _isClockedIn;
			set => SetProperty(ref _isClockedIn, value);
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

		ButtonModel _clockInOutButtonModel;

		public ButtonModel ClockInOutButtonModel
		{
			get => _clockInOutButtonModel;
			set => SetProperty(ref _clockInOutButtonModel, value);
		}

		private readonly Timer _timer;
		private ObservableCollection<WorkItem> _workItems;
		private readonly IAccountService _accountService;
		private readonly IWorkService _workService;

		public TimeClockPageModel(IAccountService accountService,
			IWorkService workService)
		{
			_accountService = accountService;
			_workService = workService;
			ClockInOutButtonModel = new ButtonModel("Clock In", OnClockInOutAction);
			_timer = new Timer
			{
				Interval = 1000,
				Enabled = false
			};
			_timer.Elapsed += OnTimerElapsed;
		}

		private void OnTimerElapsed(object sender, ElapsedEventArgs e) => RunningTotal += TimeSpan.FromSeconds(1);

		public override async Task InitializeAsync(object navigationData)
		{
			RunningTotal = new TimeSpan();
			await _accountService.GetCurrentPayRateAsync();
			WorkItems = await _workService.GetTodaysWorkAsync();

			await base.InitializeAsync(navigationData);
		}

		private async void OnClockInOutAction()
		{
			if (IsClockedIn)
			{
				_timer.Enabled = false;
				RunningTotal = TimeSpan.Zero;
				ClockInOutButtonModel.Text = "start timer";
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
				ClockInOutButtonModel.Text = "stop timer";
			}

			IsClockedIn = !IsClockedIn;
		}
	}
}