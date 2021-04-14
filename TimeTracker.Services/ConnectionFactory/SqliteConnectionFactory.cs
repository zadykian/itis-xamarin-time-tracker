using System.IO;
using System.Threading.Tasks;
using SQLite;
using TimeTracker.Services.Models;

namespace TimeTracker.Services.ConnectionFactory
{
	/// <summary>
	/// SQLite database connection factory.
	/// </summary>
	public class SqliteConnectionFactory
	{
		private readonly IDatabaseConfiguration databaseConfiguration;

		public SqliteConnectionFactory(IDatabaseConfiguration databaseConfiguration)
			=> this.databaseConfiguration = databaseConfiguration;

		/// <summary>
		/// Create new connection to database.
		/// </summary>
		public async Task<SQLiteAsyncConnection> Create()
		{
			var databaseFullPath = Path.Combine(databaseConfiguration.DirectoryPath, "time-tracker.db");
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