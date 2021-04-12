using System;
using System.Collections.Generic;
using TimeTracker.Pages;
using TimeTracker.Services.Account;
using TimeTracker.Services.Navigation;
using TimeTracker.Services.Work;
using TinyIoC;
using Xamarin.Forms;

namespace TimeTracker.PageModels.Base
{
	public class PageModelLocator
	{
		private static readonly TinyIoCContainer container;
		private static readonly Dictionary<Type, Type> pageModelsToPages;

		static PageModelLocator()
		{
			container = new TinyIoCContainer();
			pageModelsToPages = new Dictionary<Type, Type>();

			RegisterPageWithModel<LoginPageModel, LoginPage>();
			RegisterPageWithModel<MainPageModel, MainPage>();
			RegisterPageWithModel<TimerPageModel, TimerPage>();
			RegisterPageWithModel<ViewAllPageModel, ViewAllPage>();
			RegisterPageWithModel<ProfilePageModel, ProfilePage>();

			container.Register<INavigationService, NavigationService>();
			container.Register<IAccountService, AccountService>();
			container.Register<IWorkService, MockWorkService>();
		}

		private static void RegisterPageWithModel<TPageModel, TPage>()
			where TPageModel : PageModelBase
			where TPage : Page
		{
			pageModelsToPages.Add(typeof(TPageModel), typeof(TPage));
			container.Register<TPageModel>();
		}

		public static T Resolve<T>() where T : class => container.Resolve<T>();

		internal static Page CreatePageFor<TPageModelType>() where TPageModelType : PageModelBase
		{
			var pageType = pageModelsToPages[typeof(TPageModelType)];
			var page = (Page) Activator.CreateInstance(pageType);
			var pageModel = Resolve<TPageModelType>();
			page.BindingContext = pageModel;
			return page;
		}
	}
}