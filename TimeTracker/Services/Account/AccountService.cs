using System.Threading.Tasks;
using SQLite;
using TimeTracker.Models;

namespace TimeTracker.Services.Account
{
	/// <inheritdoc />
	internal class AccountService : IAccountService
	{
		private readonly SQLiteAsyncConnection dbConnection;

		public AccountService(SQLiteAsyncConnection dbConnection) => this.dbConnection = dbConnection;

		/// <inheritdoc />
		public User CurrentUser { get; private set; }

		/// <inheritdoc />
		async Task<bool> IAccountService.LoginAsync(UserCredentials credentials)
		{
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
		async Task<bool> IAccountService.CreateAccountAsync(User newUser)
		{
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
			await dbConnection.UpdateAsync(CurrentUser);
			return true;
		}
	}
}