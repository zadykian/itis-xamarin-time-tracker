using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.Images
{
	/// <summary>
	/// Service for images management.
	/// </summary>
	public interface IImageService
	{
		/// <summary>
		/// Add new image. 
		/// </summary>
		Task AddImageAsync(Image image);

		/// <summary>
		/// Get all images related to period with id <paramref name="trackedPeriodId"/>. 
		/// </summary>
		Task<IReadOnlyCollection<Image>> GetAllAsync(int trackedPeriodId);
	}
}