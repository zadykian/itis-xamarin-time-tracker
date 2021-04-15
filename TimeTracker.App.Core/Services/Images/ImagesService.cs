using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Services.Images;
using TimeTracker.Services.Models;

// ReSharper disable SuggestBaseTypeForParameter

namespace TimeTracker.App.Core.Services.Images
{
	/// <inheritdoc />
	internal class ImagesService : IImageService
	{
		private readonly IImageService sqliteImageService;
		private readonly IImageService webApiImageService;

		public ImagesService(
			SqliteImageService sqliteImageService,
			WebApiImageService webApiImageService)
		{
			this.sqliteImageService = sqliteImageService;
			this.webApiImageService = webApiImageService;
		}

		/// <inheritdoc />
		async Task IImageService.AddImageAsync(Image image)
		{
			await sqliteImageService.AddImageAsync(image);
			await webApiImageService.AddImageAsync(image);
		}

		/// <inheritdoc />
		async Task<IReadOnlyCollection<Image>> IImageService.GetAllAsync(int trackedPeriodId)
		{
			var localImages = await sqliteImageService.GetAllAsync(trackedPeriodId);

			if (localImages.Any())
			{
				return localImages;
			}

			var remoteImages = await webApiImageService.GetAllAsync(trackedPeriodId);
			foreach (var image in remoteImages) await sqliteImageService.AddImageAsync(image);
			return remoteImages;
		}
	}
}