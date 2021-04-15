using System;

namespace TimeTracker.App.Core.Services.Http.Configuration
{
	/// <summary>
	/// Http client configuration.
	/// </summary>
	internal interface IHttpConfiguration
	{
		/// <summary>
		/// Base address of remote service.
		/// </summary>
		Uri BaseAddress { get; }
	}
}