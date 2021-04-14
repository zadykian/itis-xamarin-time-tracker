using System;
using System.Threading.Tasks;
using TimeTracker.App.Core.Models;
using TimeTracker.App.Core.Services.ConnectionFactory;

namespace TimeTracker.App.Core.Services.Account
{
	/// <inheritdoc cref="IAccountService"/>
	internal class AccountService : SqliteServiceBase, IAccountService
	{
		private readonly IAccountService sqliteSubService;
		private readonly IAccountService webApiSubService;

		public AccountService()
		{
			sqliteSubService = new SqliteSubService();
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
		/// Account sub-service which is responsible for communication with SQLite database. 
		/// </summary>
		private class SqliteSubService : SqliteServiceBase, IAccountService
		{
			/// <inheritdoc />
			public User CurrentUser { get; private set; }

			/// <inheritdoc />
			async Task<bool> IAccountService.LoginAsync(UserCredentials credentials)
			{
				var dbConnection = await Connection.Value;

				var existingUser = await dbConnection
					.Table<User>()
					.FirstOrDefaultAsync(user => user.Username == credentials.Username && user.Password == credentials.Password);

				if (existingUser is null)
				{
					return false;
				}

				CurrentUser = existingUser;
				return true;
			}

			/// <inheritdoc />
			async Task IAccountService.LogOutAsync()
			{
				await Task.CompletedTask;
				CurrentUser = null;
			}

			/// <inheritdoc />
			async Task<bool> IAccountService.CreateAccountAsync(User newUser)
			{
				var dbConnection = await Connection.Value;

				var existingUser = await dbConnection
					.Table<User>()
					.FirstOrDefaultAsync(user => user.Username == newUser.Username);

				if (existingUser != null)
				{
					return false;
				}

				await dbConnection.InsertAsync(newUser);
				CurrentUser = newUser;
				return true;
			}

			/// <inheritdoc />
			async Task<bool> IAccountService.UpdatePasswordAsync(UserCredentials credentials)
			{
				if (credentials.Password == CurrentUser.Password)
				{
					return false;
				}

				CurrentUser.Password = credentials.Password;
				var dbConnection = await Connection.Value;
				await dbConnection.UpdateAsync(CurrentUser);
				return true;
			}
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