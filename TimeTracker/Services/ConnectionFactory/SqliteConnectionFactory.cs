using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using TimeTracker.Models;

namespace TimeTracker.Services.ConnectionFactory
{
	/// <summary>
	/// SQLite database connection factory.
	/// </summary>
	internal static class SqliteConnectionFactory
	{
		/// <summary>
		/// Create new connection.
		/// </summary>
		public static SQLiteAsyncConnection Create()
		{
			var databaseDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			var databaseFullPath = Path.Combine(databaseDirectory, "time-tracker.db");
			var dbConnection = new SQLiteAsyncConnection(databaseFullPath);
			Initialize(dbConnection).ConfigureAwait(false).GetAwaiter().GetResult();
			return dbConnection;
		}

		/// <summary>
		/// Initialize database schema.
		/// </summary>
		private static async Task Initialize(SQLiteAsyncConnection dbConnection)
		{
			await dbConnection.CreateTableAsync<User>();
			await dbConnection.CreateTableAsync<TrackedPeriod>();
			await dbConnection.CreateTableAsync<Image>();
		}
	}
}