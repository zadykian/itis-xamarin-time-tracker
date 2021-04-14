using System.IO;
using System.Threading.Tasks;
using SQLite;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.ConnectionFactory
{
	/// <summary>
	/// SQLite database connection factory.
	/// </summary>
	internal static class SqliteConnectionFactory
	{
		/// <summary>
		/// Create new connection to database located at <paramref name="directoryPath"/>.
		/// </summary>
		public static async Task<SQLiteAsyncConnection> Create(string directoryPath)
		{
			var databaseFullPath = Path.Combine(directoryPath, "time-tracker.db");
			var dbConnection = new SQLiteAsyncConnection(databaseFullPath);
			await Initialize(dbConnection);
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