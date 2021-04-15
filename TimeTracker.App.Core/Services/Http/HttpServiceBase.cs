using System.Net.Http;
using System.Threading.Tasks;

namespace TimeTracker.App.Core.Services.Http
{
	/// <summary>
	/// Base class for services which performs http requests.
	/// </summary>
	internal abstract class HttpServiceBase
	{
		private readonly HttpClient httpClient;

		protected HttpServiceBase(HttpClient httpClient) => this.httpClient = httpClient;

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