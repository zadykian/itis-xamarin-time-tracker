using System.Threading.Tasks;
using TimeTracker.PageModels.Base;

namespace TimeTracker.PageModels
{
	internal class MainPageModel : PageModelBase
	{
		public MainPageModel(ProfilePageModel profilePM,
			SummaryPageModel summaryPM,
			TimeClockPageModel timePM)
		{
			ProfilePageModel = profilePM;
			SummaryPageModel = summaryPM;
			TimeClockPageModel = timePM;
		}

		private ProfilePageModel _profilePM;

		public ProfilePageModel ProfilePageModel
		{
			get => _profilePM;
			set => SetProperty(ref _profilePM, value);
		}

		private SummaryPageModel _summaryPM;

		public SummaryPageModel SummaryPageModel
		{
			get => _summaryPM;
			set => SetProperty(ref _summaryPM, value);
		}

		private TimeClockPageModel _timePM;

		public TimeClockPageModel TimeClockPageModel
		{
			get => _timePM;
			set => SetProperty(ref _timePM, value);
		}

		public override Task InitializeAsync(object navigationData)
			=> Task.WhenAny(
				base.InitializeAsync(navigationData),
				ProfilePageModel.InitializeAsync(null),
				SummaryPageModel.InitializeAsync(null),
				TimeClockPageModel.InitializeAsync(null));
	}
}