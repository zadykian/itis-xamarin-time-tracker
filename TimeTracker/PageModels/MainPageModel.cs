using System.Threading.Tasks;
using TimeTracker.PageModels.Base;

namespace TimeTracker.PageModels
{
	internal class MainPageModel : PageModelBase
	{
		public MainPageModel(
			TimerPageModel timerPageModel,
			ProfilePageModel profilePM,
			ViewAllPageModel viewAllPm)
		{
			ProfilePageModel = profilePM;
			ViewAllPageModel = viewAllPm;
			TimerPageModel = timerPageModel;
		}

		private TimerPageModel timerPageModel;

		public TimerPageModel TimerPageModel
		{
			get => timerPageModel;
			set => SetProperty(ref timerPageModel, value);
		}

		private ProfilePageModel profilePageModel;

		public ProfilePageModel ProfilePageModel
		{
			get => profilePageModel;
			set => SetProperty(ref profilePageModel, value);
		}

		private ViewAllPageModel viewAllPageModel;

		public ViewAllPageModel ViewAllPageModel
		{
			get => viewAllPageModel;
			set => SetProperty(ref viewAllPageModel, value);
		}

		public override Task InitializeAsync(object navigationData)
			=> Task.WhenAny(
				base.InitializeAsync(navigationData),
				ProfilePageModel.InitializeAsync(null),
				ViewAllPageModel.InitializeAsync(null),
				TimerPageModel.InitializeAsync(null));
	}
}