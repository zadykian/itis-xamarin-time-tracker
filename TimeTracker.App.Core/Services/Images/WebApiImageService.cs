using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.App.Core.Services.Http;
using TimeTracker.App.Core.Services.Http.Configuration;
using TimeTracker.Services.Images;
using TimeTracker.Services.Models;

namespace TimeTracker.App.Core.Services.Images
{
	/// <summary>
	/// Image sub-service which is responsible for communication with remote Web API.
	/// </summary>
	internal class WebApiImageService : HttpServiceBase, IImageService
	{
		public WebApiImageService(IHttpConfiguration httpConfiguration) : base(httpConfiguration)
		{
		}

		/// <inheritdoc />
		async Task IImageService.AddImageAsync(Image image)
			=> await CallAsync(HttpMethod.Post, "Image", "AddImage", image);

		/// <inheritdoc />
		async Task<IReadOnlyCollection<Image>> IImageService.GetAllAsync(int trackedPeriodId)
		{
			var httpResponse = await CallAsync(HttpMethod.Get, "Image", "GetAll", $"{nameof(trackedPeriodId)}={trackedPeriodId}");
			return await httpResponse.FromBody<IReadOnlyCollection<Image>>() ?? ArraySegment<Image>.Empty;
		}
	}
}