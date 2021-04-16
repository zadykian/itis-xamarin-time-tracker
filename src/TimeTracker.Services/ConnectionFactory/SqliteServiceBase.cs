using System;
using System.Threading;
using System.Threading.Tasks;
using SQLite;

namespace TimeTracker.Services.ConnectionFactory
{
	/// <summary>
	/// Base class for services which communicate with SQLite database.
	/// </summary>
	public abstract class SqliteServiceBase
	{
		protected SqliteServiceBase(SqliteConnectionFactory sqliteConnectionFactory)
			=> Connection = new Lazy<Task<SQLiteAsyncConnection>>(
				sqliteConnectionFactory.Create,
				LazyThreadSafetyMode.PublicationOnly);

		/// <summary>
		/// Database connection.
		/// </summary>
		protected Lazy<Task<SQLiteAsyncConnection>> Connection { get; }
	}
}