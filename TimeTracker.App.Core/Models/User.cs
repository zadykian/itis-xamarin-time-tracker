using SQLite;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace TimeTracker.App.Core.Models
{
	/// <summary>
	/// User.
	/// </summary>
	internal class User : UserCredentials
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