using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeTracker.App.Core.Services.Http.Configuration;
using static System.Net.Mime.MediaTypeNames;

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

		protected async Task<HttpResponseMessage> CallAsync<TBody>(
			HttpMethod httpMethod,
			string controllerName,
			string actionName,
			TBody body)
		{
			var httpRequest = new HttpRequestMessage(httpMethod, $"{controllerName}/{actionName}")
			{
				Content = new StringContent(
					JsonConvert.SerializeObject(body),
					Encoding.UTF8,
					Application.Json)
			};

			try
			{
				return await httpClient.SendAsync(httpRequest);
			}
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}
		
		protected async Task<HttpResponseMessage> CallAsync(
			HttpMethod httpMethod,
			string controllerName,
			string actionName,
			string queryParameters)
		{
			var httpRequest = new HttpRequestMessage(httpMethod, $"{controllerName}/{actionName}?{queryParameters}");

			try
			{
				return await httpClient.SendAsync(httpRequest);
			}
			catch
			{
				return new HttpResponseMessage(HttpStatusCode.InternalServerError);
			}
		}
	}

	internal static class HttpResponseMessageExtensions
	{
		[return: MaybeNull]
		public static async Task<T> FromBody<T>(this HttpResponseMessage httpResponseMessage)
			where T : class
		{
			if (httpResponseMessage.StatusCode != HttpStatusCode.OK) return null;
			var bodyString = await httpResponseMessage.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(bodyString);
		}
	}
}