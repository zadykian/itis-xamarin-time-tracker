using SQLite;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable UnusedMember.Global

namespace TimeTracker.Services.Models
{
	/// <summary>
	/// User.
	/// </summary>
	public class User : UserCredentials
	{
		public User()
		{
		}

		public User(string username, string password)
			: base(username, password)
		{
		}

		/// <summary>
		/// User's identifier.
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }
	}
}