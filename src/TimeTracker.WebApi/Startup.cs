using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.Services.Account;
using TimeTracker.Services.ConnectionFactory;
using TimeTracker.Services.Images;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.WebApi
{
	public class Startup
	{
		/// <inheritdoc />
		private sealed class EnvironmentDatabaseConfiguration : IDatabaseConfiguration
		{
			/// <inheritdoc />
			string IDatabaseConfiguration.DirectoryPath => Environment.CurrentDirectory;
		}

		public void ConfigureServices(IServiceCollection services)
			=> services
				.AddSingleton<IDatabaseConfiguration, EnvironmentDatabaseConfiguration>()
				.AddSingleton<SqliteConnectionFactory>()
				.AddSingleton<IAccountService, SqliteAccountService>()
				.AddSingleton<ITrackedPeriodService, SqliteTrackedPeriodService>()
				.AddSingleton<IImageService, SqliteImageService>()
				.AddControllers();

		public void Configure(IApplicationBuilder app)
			=> app
				.UseRouting()
				.UseEndpoints(builder => builder.MapControllers());
	}
}