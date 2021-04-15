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

			var succeededRemotely = await webApiAccountService.LogInAsync(credentials);
			if (!succeededRemotely) return false;

			var updatedLocally = await sqliteAccountService.UpdatePasswordAsync(credentials);
			if (updatedLocally) return await sqliteAccountService.LogInAsync(credentials);

			var createdLocally = await sqliteAccountService.CreateAccountAsync(credentials);
			return createdLocally && await sqliteAccountService.LogInAsync(credentials);
		}

		/// <inheritdoc />
		async Task IAccountService.LogOutAsync()
		{
			await sqliteAccountService.LogOutAsync();
			await webApiAccountService.LogOutAsync();
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.CreateAccountAsync(UserCredentials credentials)
		{
			var succeededLocally = await sqliteAccountService.CreateAccountAsync(credentials);
			var succeededRemotely = await webApiAccountService.CreateAccountAsync(credentials);
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