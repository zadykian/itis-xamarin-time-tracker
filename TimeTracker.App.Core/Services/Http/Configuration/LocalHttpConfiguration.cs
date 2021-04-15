using System;

namespace TimeTracker.App.Core.Services.Http.Configuration
{
	/// <inheritdoc />
	internal class LocalHttpConfiguration : IHttpConfiguration
	{
		/// <inheritdoc />
		Uri IHttpConfiguration.BaseAddress => new Uri("http://localhost:5000");
	}
}