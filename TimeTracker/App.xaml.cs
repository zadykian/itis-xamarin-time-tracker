using System.Threading.Tasks;
using TimeTracker.PageModels;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Navigation;

namespace TimeTracker
{
	public partial class App
	{
		public App() => InitializeComponent();

		private static Task InitNavigation()
		{
			var navigationService = PageModelLocator.Resolve<INavigationService>();
			return navigationService.NavigateToAsync<LoginPageModel>(setRoot: true);
		}

		protected override async void OnStart()
		{
			base.OnStart();
			await InitNavigation();
			base.OnResume();
		}

		protected override void OnSleep()
		{
		}
	}
}