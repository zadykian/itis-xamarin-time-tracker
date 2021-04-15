using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Services.Images;
using TimeTracker.Services.Models;
using TimeTracker.WebApi.Controllers.Base;

namespace TimeTracker.WebApi.Controllers
{
	/// <summary>
	/// Controller for images management.
	/// </summary>
	public class ImageController : ApiControllerBase
	{
		private readonly IImageService imageService;

		public ImageController(IImageService imageService)
			=> this.imageService = imageService;

		/// <inheritdoc cref="IImageService.AddImageAsync"/>
		[HttpPost]
		public async Task<IActionResult> AddImageAsync([FromBody, Required] Image image)
		{
			await imageService.AddImageAsync(image);
			return Ok();
		}

		/// <inheritdoc cref="IImageService.GetAllAsync"/>
		[HttpGet]
		public async Task<IActionResult> GetAllAsync([FromQuery, Required] int? trackedPeriodId)
		{
			var allImages = await imageService.GetAllAsync(trackedPeriodId!.Value);
			return Ok(allImages);
		}
	}
}