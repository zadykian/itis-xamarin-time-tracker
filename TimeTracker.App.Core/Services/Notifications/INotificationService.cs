using System;
using System.Threading.Tasks;

namespace TimeTracker.App.Core.Services.Notifications
{
	/// <summary>
	/// Notifications service.
	/// </summary>
	internal interface INotificationService
	{
		/// <summary>
		/// Push notification with info about current period. 
		/// </summary>
		Task PushNotificationAsync(TimeSpan currentElapsed);
	}
}