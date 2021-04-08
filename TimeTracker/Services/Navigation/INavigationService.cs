﻿using System.Threading.Tasks;
using TimeTracker.PageModels.Base;

namespace TimeTracker.Services.Navigation
{
	public interface INavigationService
	{
		/// <summary>
		/// Navigation method to asynchonously navigate between Page Models,
		/// and optionally pass navigation Data.
		/// </summary>
		Task NavigateToAsync<TPageModel>(object navigationData = null, bool setRoot = false)
			where TPageModel : PageModelBase;
	}
}