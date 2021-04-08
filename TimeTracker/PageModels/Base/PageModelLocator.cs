using System;
using System.Collections.Generic;
using TimeTracker.Models;
using TimeTracker.Pages;
using TimeTracker.Services;
using TimeTracker.Services.Account;
using TimeTracker.Services.Navigation;
using TimeTracker.Services.Statement;
using TimeTracker.Services.Work;
using TinyIoC;
using Xamarin.Forms;

namespace TimeTracker.PageModels.Base
{
    public class PageModelLocator
    {
        static TinyIoCContainer _container;
        static Dictionary<Type, Type> _lookupTable;

        static PageModelLocator()
        {
            _container = new TinyIoCContainer();
            _lookupTable = new Dictionary<Type, Type>();

            // Register Page and Page Models
            Register<DashboardPageModel, DashboardPage>();
            Register<LoginPageModel, LoginPage>();
            Register<LoginEmailPageModel, LoginEmailPage>();
            Register<ProfilePageModel, ProfilePage>();
            Register<SettingsPageModel, SettingsPage>();
            Register<SummaryPageModel, SummaryPage>();
            Register<TimeClockPageModel, TimeClockPage>();
            Register<RecentActivityPageModel, RecentActivityPage>();

            // Register Services (registered as Singletons by default)
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IAccountService, MockAccountService>();
            _container.Register<IStatementService, MockStatementService>();
            _container.Register<IWorkService, MockWorkService>();
            //_container.Register(DependencyService.Get<IRepository<WorkItem>>());
            _container.Register(DependencyService.Get<IRepository<TestData>>());
        }

        /// <summary>
        /// Private utility method to Register page and page model for page retrieval by it's
        /// specified page model type.
        /// </summary>
        private static void Register<TPageModel, TPage>() where TPageModel : PageModelBase where TPage : Page
        {
            _lookupTable.Add(typeof(TPageModel), typeof(TPage));
            _container.Register<TPageModel>();
        }

        public static T Resolve<T>() where T : class => _container.Resolve<T>();

        public static Page CreatePageFor<TPageModelType>() where TPageModelType : PageModelBase
        {
            var pageType = _lookupTable[typeof(TPageModelType)];
            var page = (Page)Activator.CreateInstance(pageType);
            var pageModel = Resolve<TPageModelType>();
            page.BindingContext = pageModel;
            return page;
        }
    }
}
