using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TimeTracker.App.Core.Services.UserLocation
{
	/// <summary>
	/// Service for location access.
	/// </summary>
	internal interface ILocationService
	{
		/// <summary>
		/// Get current device location.
		/// </summary>
		Task<Location> GetCurrentLocationAsync();
	}
}