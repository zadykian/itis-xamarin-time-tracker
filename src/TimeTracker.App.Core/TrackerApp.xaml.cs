using System.Threading.Tasks;
using TimeTracker.App.Core.PageModels;
using TimeTracker.App.Core.Services.Navigation;

namespace TimeTracker.App.Core
{
	public partial class TrackerApp
	{
		public TrackerApp() => InitializeComponent();

		protected override async void OnStart()
		{
			base.OnStart();
			await InitNavigation();
			base.OnResume();
		}

		private static Task InitNavigation()
			=> AppContext
				.Resolve<INavigationService>()
				.NavigateToAsync<LoginPageModel>(setRoot: true);
	}
}