using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TimeTracker.Services.UserLocation
{
	/// <inheritdoc />
	internal class LocationService : ILocationService
	{
		/// <inheritdoc />
		async Task<Location> ILocationService.GetCurrentLocationAsync()
		{
			// todo
			// return await Geolocation.GetLocationAsync();
			await Task.CompletedTask;
			return new Location(50, 50);
		}
	}
}