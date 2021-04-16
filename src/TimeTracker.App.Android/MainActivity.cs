using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.Fingerprint;
using Plugin.LocalNotification;
using TimeTracker.App.Core;

namespace TimeTracker.App.Android
{
	/// <inheritdoc />
	[Activity(Label = "TimeTracker", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		/// <inheritdoc />
		protected override void OnCreate(Bundle savedInstanceState)
		{
			CrossFingerprint.SetCurrentActivityResolver(() => this);

			NotificationCenter.CreateNotificationChannel(
				new Plugin.LocalNotification.Platform.Droid.NotificationChannelRequest
				{
					Id = "time-tracker-notifications",
					Name = "General",
					Description = "General",
				});

			base.OnCreate(savedInstanceState);

			Xamarin.Essentials.Platform.Init(this, savedInstanceState);
			Xamarin.Forms.Forms.Init(this, savedInstanceState);
			LoadApplication(new TrackerApp());
		}

		/// <inheritdoc />
		public override void OnRequestPermissionsResult(
			int requestCode,
			string[] permissions,
			[GeneratedEnum] Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}