using System;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Account
{
	/// <inheritdoc />
	internal class AccountService : IAccountService
	{
		/// <inheritdoc />
		public User CurrentUser { get; private set; }

		/// <inheritdoc />
		async Task<bool> IAccountService.LoginAsync(UserCredentials credentials)
		{
			// todo
			await Task.CompletedTask;
			CurrentUser = new User(Guid.NewGuid(), credentials.Username, credentials.Password);
			return true;
		}

		/// <inheritdoc />
		async Task<bool> IAccountService.CreateAccountAsync(User user)
		{
			// todo
			await Task.CompletedTask;
			CurrentUser = user;
			return true;
		}
	}
}