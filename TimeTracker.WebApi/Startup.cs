using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimeTracker.Services.Account;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.WebApi
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
			=> services
				.AddSingleton<IAccountService>(_ => new SqliteAccountService(Environment.CurrentDirectory))
				.AddSingleton<ITrackedPeriodService>(_ => new SqliteTrackedPeriodService(Environment.CurrentDirectory));

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
			});
		}
	}
}