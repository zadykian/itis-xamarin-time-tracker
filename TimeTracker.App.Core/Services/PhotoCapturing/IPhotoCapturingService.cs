using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTracker.App.Core.Services.PhotoCapturing
{
	/// <summary>
	/// Service for device's camera access.
	/// </summary>
	internal interface IPhotoCapturingService
	{
		/// <summary>
		/// Capture image using device's camera.
		/// </summary>
		Task<IReadOnlyCollection<byte>> CapturePhotoAsync();
	}
}