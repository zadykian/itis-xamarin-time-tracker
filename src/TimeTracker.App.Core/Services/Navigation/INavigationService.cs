using System.Threading.Tasks;
using TimeTracker.App.Core.PageModels.Base;

namespace TimeTracker.App.Core.Services.Navigation
{
	internal interface INavigationService
	{
		/// <summary>
		/// Navigation method to asynchronously navigate between Page Models,
		/// and optionally pass navigation Data.
		/// </summary>
		Task NavigateToAsync<TPageModel>(bool setRoot = false)
			where TPageModel : PageModelBase;
	}
}