using System;
using System.Collections.Generic;
using TimeTracker.App.Core.PageModels;
using TimeTracker.App.Core.PageModels.Base;
using TimeTracker.App.Core.Pages;
using TimeTracker.App.Core.Services.Account;
using TimeTracker.App.Core.Services.Navigation;
using TimeTracker.App.Core.Services.Notifications;
using TimeTracker.App.Core.Services.PhotoCapturing;
using TimeTracker.App.Core.Services.TimeTracking;
using TimeTracker.App.Core.Services.UserLocation;
using TimeTracker.Services.Account;
using TimeTracker.Services.TimeTracking;
using TinyIoC;
using Xamarin.Forms;

namespace TimeTracker.App.Core
{
	/// <summary>
	/// Application global context.
	/// </summary>
	internal static class AppContext
	{
		private static readonly TinyIoCContainer container;
		private static readonly Dictionary<Type, Type> pageModelsToPages;

		static AppContext()
		{
			container = new TinyIoCContainer();
			pageModelsToPages = new Dictionary<Type, Type>();

			RegisterPageWithModel<LoginPageModel, LoginPage>();
			RegisterPageWithModel<MainPageModel, MainPage>();
			RegisterPageWithModel<TimerPageModel, TimerPage>();
			RegisterPageWithModel<ViewAllPageModel, ViewAllPage>();
			RegisterPageWithModel<ProfilePageModel, ProfilePage>();

			container.Register<IAccountService, AccountService>();
			container.Register<INavigationService, NavigationService>();

			container.Register<ITrackedPeriodService, TrackedPeriodService>();
			container.Register<ILocationService, LocationService>();
			container.Register<IPhotoCapturingService, PhotoCapturingService>();
			container.Register<INotificationService, NotificationService>();
		}

		private static void RegisterPageWithModel<TPageModel, TPage>()
			where TPageModel : PageModelBase
			where TPage : Page
		{
			pageModelsToPages.Add(typeof(TPageModel), typeof(TPage));
			container.Register<TPageModel>().AsSingleton();
		}

		public static T Resolve<T>() where T : class => container.Resolve<T>();

		internal static Page CreatePageFor<TPageModelType>() where TPageModelType : PageModelBase
		{
			var pageType = pageModelsToPages[typeof(TPageModelType)];
			var page = (Page) Activator.CreateInstance(pageType);
			page.BindingContext = Resolve<TPageModelType>();
			return page;
		}
	}
}