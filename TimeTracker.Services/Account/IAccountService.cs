using System.Threading.Tasks;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.Account
{
	/// <summary>
	/// Service for users' accounts management.
	/// </summary>
	public interface IAccountService
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
		/// Log out.
		/// </summary>
		Task LogOutAsync();

		/// <summary>
		/// Create new account. 
		/// </summary>
		Task<bool> CreateAccountAsync(User user);

		/// <summary>
		/// Update user's password. 
		/// </summary>
		Task<bool> UpdatePasswordAsync(UserCredentials credentials);
	}
}