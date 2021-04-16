namespace TimeTracker.Services.ConnectionFactory
{
	/// <summary>
	/// SQLite database configuration.
	/// </summary>
	public interface IDatabaseConfiguration
	{
		/// <summary>
		/// Database directory full path.
		/// </summary>
		string DirectoryPath { get; }
	}
}