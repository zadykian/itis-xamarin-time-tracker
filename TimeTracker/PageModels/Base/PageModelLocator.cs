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
		private static readonly TinyIoCContainer _container;
		private static readonly Dictionary<Type, Type> _lookupTable;

		static PageModelLocator()
		{
			_container = new TinyIoCContainer();
			_lookupTable = new Dictionary<Type, Type>();

			Register<LoginPageModel, LoginPage>();

			Register<MainPageModel, MainPage>();
			Register<ProfilePageModel, ProfilePage>();
			Register<SummaryPageModel, SummaryPage>();
			Register<TimeClockPageModel, TimeClockPage>();
			Register<RecentActivityPageModel, RecentActivityPage>();

			_container.Register<INavigationService, NavigationService>();
			_container.Register<IAccountService, MockAccountService>();
			_container.Register<IStatementService, MockStatementService>();
			_container.Register<IWorkService, MockWorkService>();
			//_container.Register(DependencyService.Get<IRepository<WorkItem>>());
			_container.Register(DependencyService.Get<IRepository<TestData>>());
		}

		private static void Register<TPageModel, TPage>()
			where TPageModel : PageModelBase
			where TPage : Page
		{
			_lookupTable.Add(typeof(TPageModel), typeof(TPage));
			_container.Register<TPageModel>();
		}

		public static T Resolve<T>() where T : class => _container.Resolve<T>();

		internal static Page CreatePageFor<TPageModelType>() where TPageModelType : PageModelBase
		{
			var pageType = _lookupTable[typeof(TPageModelType)];
			var page = (Page) Activator.CreateInstance(pageType);
			var pageModel = Resolve<TPageModelType>();
			page.BindingContext = pageModel;
			return page;
		}
	}
}