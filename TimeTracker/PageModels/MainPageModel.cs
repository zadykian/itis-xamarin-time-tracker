using System.Threading.Tasks;
using TimeTracker.PageModels.Base;

namespace TimeTracker.PageModels
{
	internal class MainPageModel : PageModelBase
	{
		public MainPageModel(
			TimeClockPageModel timeClockPageModel,
			ProfilePageModel profilePM,
			SummaryPageModel summaryPM)
		{
			ProfilePageModel = profilePM;
			SummaryPageModel = summaryPM;
			TimeClockPageModel = timeClockPageModel;
		}

		private TimeClockPageModel timeClockPageModel;

		public TimeClockPageModel TimeClockPageModel
		{
			get => timeClockPageModel;
			set => SetProperty(ref timeClockPageModel, value);
		}

		private ProfilePageModel profilePageModel;

		public ProfilePageModel ProfilePageModel
		{
			get => profilePageModel;
			set => SetProperty(ref profilePageModel, value);
		}

		private SummaryPageModel summaryPageModel;

		public SummaryPageModel SummaryPageModel
		{
			get => summaryPageModel;
			set => SetProperty(ref summaryPageModel, value);
		}

		public override Task InitializeAsync(object navigationData)
			=> Task.WhenAny(
				base.InitializeAsync(navigationData),
				ProfilePageModel.InitializeAsync(null),
				SummaryPageModel.InitializeAsync(null),
				TimeClockPageModel.InitializeAsync(null));
	}
}