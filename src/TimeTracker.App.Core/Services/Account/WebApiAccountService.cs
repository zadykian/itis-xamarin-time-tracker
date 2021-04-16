using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TimeTracker.App.Core.Services.Http;
using TimeTracker.App.Core.Services.Http.Configuration;
using TimeTracker.Services.Account;
using TimeTracker.Services.Models;

namespace TimeTracker.App.Core.Services.Account
{
	/// <summary>
	/// Account sub-service which is responsible for communication with remote Web API.
	/// </summary>
	internal class WebApiAccountService : HttpServiceBase, IAccountService
	{
		public WebApiAccountService(IHttpConfiguration httpConfiguration) : base(httpConfiguration)
		{
		}

		/// <inheritdoc />
		User IAccountService.CurrentUser => null;

		/// <inheritdoc />
		async Task<bool> IAccountService.LogInAsync(UserCredentials credentials)
		{
			var httpResponse = await CallAsync(HttpMethod.Put, "Account", "LogIn", credentials);
			return httpResponse.StatusCode == HttpStatusCode.OK;
		}

		/// <inheritdoc />
		Task IAccountService.LogOutAsync() => Task.CompletedTask;

		/// <inheritdoc />
		async Task<bool> IAccountService.CreateAccountAsync(UserCredentials credentials)
		{
			var httpResponse = await CallAsync(HttpMethod.Post, "Account", "CreateAccount", credentials);
			return httpResponse.StatusCode == HttpStatusCode.OK;
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.UpdatePasswordAsync(UserCredentials credentials)
		{
			var httpResponse = await CallAsync(HttpMethod.Put, "Account", "UpdatePassword", credentials);
			return httpResponse.StatusCode == HttpStatusCode.OK;
		}
	}
}