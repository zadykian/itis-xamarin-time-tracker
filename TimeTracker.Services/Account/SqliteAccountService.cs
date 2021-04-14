using System.Threading.Tasks;
using TimeTracker.Services.ConnectionFactory;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.Account
{
	/// <inheritdoc cref="IAccountService"/>
	public class SqliteAccountService : SqliteServiceBase, IAccountService
	{
		public SqliteAccountService(SqliteConnectionFactory sqliteConnectionFactory)
			: base(sqliteConnectionFactory)
		{
		}

		/// <inheritdoc />
		public User CurrentUser { get; private set; }

		/// <inheritdoc />
		async Task<bool> IAccountService.LogInAsync(UserCredentials credentials)
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
}