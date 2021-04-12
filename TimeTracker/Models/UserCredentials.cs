namespace TimeTracker.Models
{
	/// <summary>
	/// User's credentials.
	/// </summary>
	internal class UserCredentials
	{
		public UserCredentials(string username, string password)
		{
			Username = username;
			Password = password;
		}

		/// <summary>
		/// User's login.
		/// </summary>
		public string Username { get; }

		/// <summary>
		/// User's password.
		/// </summary>
		public string Password { get; }
	}
}