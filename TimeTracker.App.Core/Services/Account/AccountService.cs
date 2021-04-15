using System;
using System.Threading.Tasks;
using TimeTracker.Services.Account;
using TimeTracker.Services.Models;

// ReSharper disable SuggestBaseTypeForParameter

namespace TimeTracker.App.Core.Services.Account
{
	/// <inheritdoc cref="IAccountService"/>
	internal class AccountService : IAccountService
	{
		private readonly IAccountService sqliteAccountService;
		private readonly IAccountService webApiAccountService;

		public AccountService(
			SqliteAccountService sqliteAccountService,
			WebApiAccountService webApiAccountService)
		{
			this.sqliteAccountService = sqliteAccountService;
			this.webApiAccountService = webApiAccountService;
		}

		/// <inheritdoc />
		User IAccountService.CurrentUser
			=> sqliteAccountService.CurrentUser
			   ?? webApiAccountService.CurrentUser
			   ?? throw new InvalidOperationException("Current user is not initialized.");

		/// <inheritdoc />
		async Task<bool> IAccountService.LogInAsync(UserCredentials credentials)
		{
			var succeededLocally = await sqliteAccountService.LogInAsync(credentials);
			if (succeededLocally) return true;
			return await webApiAccountService.LogInAsync(credentials);
		}

		/// <inheritdoc />
		async Task IAccountService.LogOutAsync()
		{
			await sqliteAccountService.LogOutAsync();
			await webApiAccountService.LogOutAsync();
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.CreateAccountAsync(User newUser)
		{
			var succeededLocally = await sqliteAccountService.CreateAccountAsync(newUser);
			var succeededRemotely = await webApiAccountService.CreateAccountAsync(newUser);
			return succeededLocally && succeededRemotely;
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.UpdatePasswordAsync(UserCredentials credentials)
		{
			var succeededLocally = await sqliteAccountService.UpdatePasswordAsync(credentials);
			var succeededRemotely = await webApiAccountService.UpdatePasswordAsync(credentials);
			return succeededLocally && succeededRemotely;
		}
	}
}