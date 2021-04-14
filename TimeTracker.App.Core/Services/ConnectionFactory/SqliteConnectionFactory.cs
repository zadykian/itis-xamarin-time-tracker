using System.IO;
using System.Threading.Tasks;
using SQLite;
using TimeTracker.App.Core.Models;

namespace TimeTracker.App.Core.Services.ConnectionFactory
{
	/// <summary>
	/// SQLite database connection factory.
	/// </summary>
	internal static class SqliteConnectionFactory
	{
		/// <summary>
		/// Create new connection.
		/// </summary>
		public static async Task<SQLiteAsyncConnection> Create()
		{
			var databaseFullPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "time-tracker.db");
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