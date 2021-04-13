using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TimeTracker.Services.Photo
{
	/// <inheritdoc />
	internal class PhotoService : IPhotoService
	{
		/// <inheritdoc />
		async Task<IReadOnlyCollection<byte>> IPhotoService.CapturePhotoAsync()
		{
			var fileResult = await MediaPicker.CapturePhotoAsync();

			if (fileResult is null)
			{
				return ArraySegment<byte>.Empty;
			}

			var fileStream = await fileResult.OpenReadAsync();
			var memoryStream = new MemoryStream();
			await fileStream.CopyToAsync(memoryStream);
			return memoryStream.ToArray();
		}
	}
}