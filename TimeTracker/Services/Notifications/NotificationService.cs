using System;
using System.Threading.Tasks;

namespace TimeTracker.Services.Notifications
{
	/// <inheritdoc />
	internal class NotificationService : INotificationService
	{
		/// <inheritdoc />
		async Task INotificationService.PushNotificationAsync(TimeSpan currentElapsed)
		{
			// todo
			await Task.CompletedTask;
		}
	}
}