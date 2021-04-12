using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimeTracker.Services.Photo
{
	/// <inheritdoc />
	internal class PhotoService : IPhotoService
	{
		/// <inheritdoc />
		async Task<IReadOnlyCollection<byte>> IPhotoService.CapturePhotoAsync()
		{
			await Task.CompletedTask;
			return new byte[] { 10, 20, 30, 40 };
		}
	}
}