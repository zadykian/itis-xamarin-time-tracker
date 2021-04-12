using System;

namespace TimeTracker.Models
{
	/// <summary>
	/// User.
	/// </summary>
	internal class User : UserCredentials
	{
		public User(Guid id, string username, string password)
			: base(username, password)
			=> Id = id;

		public User(string username, string password)
			: this(Guid.NewGuid(), username, password)
		{
		}

		/// <summary>
		/// User's identifier.
		/// </summary>
		public Guid Id { get; }
	}
}