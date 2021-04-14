using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Services.ConnectionFactory;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.Images
{
	/// <inheritdoc cref="IImageService"/>
	public class SqliteImageService : SqliteServiceBase, IImageService
	{
		public SqliteImageService(SqliteConnectionFactory sqliteConnectionFactory)
			: base(sqliteConnectionFactory)
		{
		}

		/// <inheritdoc />
		async Task IImageService.AddImageAsync(Image image)
			=> await (await Connection.Value).InsertAsync(image);

		/// <inheritdoc />
		async Task<IReadOnlyCollection<Image>> IImageService.GetAllAsync(int trackedPeriodId)
			=> await (await Connection.Value)
				.Table<Image>()
				.Where(image => image.TrackedPeriodId == trackedPeriodId)
				.ToArrayAsync();
	}
}