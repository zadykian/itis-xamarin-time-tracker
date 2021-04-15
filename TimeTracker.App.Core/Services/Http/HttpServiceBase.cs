using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.App.Core.Services.Http.Configuration;

namespace TimeTracker.App.Core.Services.Http
{
	/// <summary>
	/// Base class for services which performs http requests.
	/// </summary>
	internal abstract class HttpServiceBase
	{
		private readonly HttpClient httpClient;

		protected HttpServiceBase(IHttpConfiguration httpConfiguration)
			=> httpClient = new HttpClient {BaseAddress = httpConfiguration.BaseAddress};

		protected async Task<HttpResponseMessage> CallAsync(
			HttpMethod httpMethod,
			string controllerName,
			string actionName)
		{
			var httpRequest = new HttpRequestMessage(httpMethod, $"{controllerName}/{actionName}");
			return await httpClient.SendAsync(httpRequest);
		}
	}
}