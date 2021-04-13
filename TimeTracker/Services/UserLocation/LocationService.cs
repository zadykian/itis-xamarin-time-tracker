using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TimeTracker.Services.UserLocation
{
	/// <inheritdoc />
	internal class LocationService : ILocationService
	{
		/// <inheritdoc />
		async Task<Location> ILocationService.GetCurrentLocationAsync() => await Geolocation.GetLocationAsync();
	}
}