using System;
using System.Threading.Tasks;
using TimeTracker.Services.Account;
using TimeTracker.Services.Models;
using Xamarin.Essentials;

namespace TimeTracker.App.Core.Services.Account
{
	/// <inheritdoc cref="IAccountService"/>
	internal class AccountService : IAccountService
	{
		private readonly IAccountService sqliteSubService;
		private readonly IAccountService webApiSubService;

		public AccountService()
		{
			sqliteSubService = new SqliteAccountService(FileSystem.AppDataDirectory);
			webApiSubService = new WebApiSubService();
		}

		/// <inheritdoc />
		User IAccountService.CurrentUser
			=> sqliteSubService.CurrentUser
			   ?? webApiSubService.CurrentUser
			   ?? throw new InvalidOperationException("Current user is not initialized.");

		/// <inheritdoc />
		async Task<bool> IAccountService.LoginAsync(UserCredentials credentials)
		{
			var succeededLocally = await sqliteSubService.LoginAsync(credentials);
			if (succeededLocally) return true;
			return await webApiSubService.LoginAsync(credentials);
		}

		/// <inheritdoc />
		async Task IAccountService.LogOutAsync()
		{
			await sqliteSubService.LogOutAsync();
			await webApiSubService.LogOutAsync();
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.CreateAccountAsync(User newUser)
		{
			var succeededLocally = await sqliteSubService.CreateAccountAsync(newUser);
			var succeededRemotely = await webApiSubService.CreateAccountAsync(newUser);
			return succeededLocally && succeededRemotely;
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.UpdatePasswordAsync(UserCredentials credentials)
		{
			var succeededLocally = await sqliteSubService.UpdatePasswordAsync(credentials);
			var succeededRemotely = await webApiSubService.UpdatePasswordAsync(credentials);
			return succeededLocally && succeededRemotely;
		}

		/// <summary>
		/// Account sub-service which is responsible for communication with remote Web API.
		/// </summary>
		private class WebApiSubService : IAccountService
		{
			// todo: implement interaction with web api via http client

			/// <inheritdoc />
			User IAccountService.CurrentUser => null;

			/// <inheritdoc />
			async Task<bool> IAccountService.LoginAsync(UserCredentials credentials)
			{
				await Task.CompletedTask;
				return false;
			}

			/// <inheritdoc />
			async Task IAccountService.LogOutAsync() => await Task.CompletedTask;

			/// <inheritdoc />
			async Task<bool> IAccountService.CreateAccountAsync(User user)
			{
				await Task.CompletedTask;
				return true;
			}

			/// <inheritdoc />
			async Task<bool> IAccountService.UpdatePasswordAsync(UserCredentials credentials)
			{
				await Task.CompletedTask;
				return false;
			}
		}
	}
}