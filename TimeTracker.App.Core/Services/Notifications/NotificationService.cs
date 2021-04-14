using System;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace TimeTracker.Services.Notifications
{
	/// <inheritdoc />
	internal class NotificationService : INotificationService
	{
		/// <inheritdoc />
		async Task INotificationService.PushNotificationAsync(TimeSpan currentElapsed)
		{
			await Task.CompletedTask;
			var notification = new NotificationRequest
			{
				NotificationId = (int) currentElapsed.TotalMilliseconds,
				Title = "Elapsed",
				Description = $@"Your current period is {currentElapsed:hh\:mm\:ss} total!",
				Android = {ChannelId = "time-tracker-notifications"}
			};
			NotificationCenter.Current.Show(notification);
		}
	}
}