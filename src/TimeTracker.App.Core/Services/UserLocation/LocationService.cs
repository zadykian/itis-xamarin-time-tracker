using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TimeTracker.App.Core.Services.UserLocation
{
	/// <inheritdoc />
	internal class LocationService : ILocationService
	{
		/// <inheritdoc />
		async Task<Location> ILocationService.GetCurrentLocationAsync() => await Geolocation.GetLocationAsync();
	}
}