using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Services.Images;
using TimeTracker.Services.Models;
using Xamarin.Essentials;

namespace TimeTracker.App.Core.Services.Images
{
	/// <inheritdoc />
	internal class ImagesService : IImageService
	{
		private readonly IImageService sqliteImageService;
		private readonly IImageService webApiImageService;

		public ImagesService()
		{
			sqliteImageService = new SqliteImageService(FileSystem.AppDataDirectory);
			webApiImageService = new WebApiSubService();
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

		/// <summary>
		/// Image sub-service which is responsible for communication with remote Web API.
		/// </summary>
		private class WebApiSubService : IImageService
		{
			// todo: implement interaction with web api via http client

			/// <inheritdoc />
			async Task IImageService.AddImageAsync(Image image) => await Task.CompletedTask;

			/// <inheritdoc />
			async Task<IReadOnlyCollection<Image>> IImageService.GetAllAsync(int trackedPeriodId)
			{
				await Task.CompletedTask;
				return ArraySegment<Image>.Empty;
			}
		}
	}
}