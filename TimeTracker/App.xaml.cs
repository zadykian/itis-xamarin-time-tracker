using System.Threading.Tasks;
using TimeTracker.PageModels;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Navigation;
using Xamarin.Forms;

namespace TimeTracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        private Task InitNavigation()
        {
            var navigationService = PageModelLocator.Resolve<INavigationService>();
            return navigationService.NavigateToAsync<LoginPageModel>(null, true);
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
