using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.App.Core.Services.Http;
using TimeTracker.Services.Images;
using TimeTracker.Services.Models;

namespace TimeTracker.App.Core.Services.Images
{
	/// <summary>
	/// Image sub-service which is responsible for communication with remote Web API.
	/// </summary>
	internal class WebApiImageService : HttpServiceBase, IImageService
	{
		public WebApiImageService(HttpClient httpClient) : base(httpClient)
		{
		}

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