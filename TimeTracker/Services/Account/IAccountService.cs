using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Account
{
	/// <summary>
	/// Service for users' accounts management.
	/// </summary>
	internal interface IAccountService
	{
		/// <summary>
		/// Current user.
		/// </summary>
		User CurrentUser { get; }

		/// <summary>
		/// Log in as existing user with <paramref name="credentials"/>. 
		/// </summary>
		Task<bool> LoginAsync(UserCredentials credentials);

		/// <summary>
		/// Create new account. 
		/// </summary>
		Task<bool> CreateAccountAsync(User user);
	}
}