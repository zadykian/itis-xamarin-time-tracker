using System;

namespace TimeTracker.Models
{
	/// <summary>
	/// User.
	/// </summary>
	public class User
	{
		public User(Guid id, string username, string password)
			: this(username, password)
			=> Id = id;

		public User(string username, string password)
		{
			Id = Guid.NewGuid();
			Username = username;
			Password = password;
		}

		/// <summary>
		/// User's identifier.
		/// </summary>
		public Guid Id { get; }

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