using System.Threading.Tasks;
using TimeTracker.PageModels;
using TimeTracker.Services.Navigation;

namespace TimeTracker
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