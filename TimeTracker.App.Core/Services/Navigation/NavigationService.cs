using System.Threading.Tasks;
using TimeTracker.App.Core.PageModels.Base;
using Xamarin.Forms;

namespace TimeTracker.App.Core.Services.Navigation
{
	/// <inheritdoc />
	internal class NavigationService : INavigationService
	{
		/// <inheritdoc />
		async Task INavigationService.NavigateToAsync<TPageModel>(bool setRoot)
		{
			var page = AppContext.CreatePageFor<TPageModel>();

			if (setRoot)
			{
				if (page is TabbedPage tabbedPage)
				{
					Application.Current.MainPage = tabbedPage;
				}
				else
				{
					Application.Current.MainPage = new NavigationPage(page);
				}
			}
			else
			{
				if (page is TabbedPage tabPage)
				{
					Application.Current.MainPage = tabPage;
				}
				else if (Application.Current.MainPage is NavigationPage navigationPage)
				{
					await navigationPage.PushAsync(page);
				}
				else if (Application.Current.MainPage is TabbedPage tabbedPage)
				{
					if (tabbedPage.CurrentPage is NavigationPage nPage)
					{
						await nPage.PushAsync(page);
					}
				}
				else
				{
					Application.Current.MainPage = new NavigationPage(page);
				}
			}

			if (page.BindingContext is PageModelBase pmBase)
			{
				await pmBase.InitializeAsync(null);
			}
		}
	}
}