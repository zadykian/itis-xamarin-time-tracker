using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.Services.Account;
using TimeTracker.Services.Images;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.WebApi
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
			=> services
				.AddSingleton<IAccountService>(_ => new SqliteAccountService(Environment.CurrentDirectory))
				.AddSingleton<ITrackedPeriodService>(_ => new SqliteTrackedPeriodService(Environment.CurrentDirectory))
				.AddSingleton<IImageService>(_ => new SqliteImageService(Environment.CurrentDirectory))
				.AddControllers();

		public void Configure(IApplicationBuilder app) => app.UseRouting();
	}
}