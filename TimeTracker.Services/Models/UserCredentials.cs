// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace TimeTracker.Services.Models
{
	/// <summary>
	/// User's credentials.
	/// </summary>
	public class UserCredentials
	{
		protected UserCredentials()
		{
		}

		public UserCredentials(string username, string password)
		{
			Username = username;
			Password = password;
		}

		/// <summary>
		/// User's login.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// User's password.
		/// </summary>
		public string Password { get; set; }
	}
}