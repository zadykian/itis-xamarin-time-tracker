using System;
using SQLite;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace TimeTracker.Services.Models
{
	/// <summary>
	/// Tracked time period.
	/// </summary>
	public class TrackedPeriod
	{
		public TrackedPeriod()
		{
		}

		public TrackedPeriod(int userId, DateTime start, Geolocation startLocation)
		{
			UserId = userId;
			Start = start;
			StartLatitude = startLocation.Latitude;
			StartLongitude = startLocation.Longitude;
		}

		/// <summary>
		/// Period's id.
		/// </summary>
		[PrimaryKey, AutoIncrement]
		public int? Id { get; set; }

		/// <summary>
		/// Id of a user which this period belongs to.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Period's start.
		/// </summary>
		public DateTime Start { get; set; }

		/// <summary>
		/// Period's end.
		/// </summary>
		public DateTime? End { get; set; }

		/// <summary>
		/// Period's total interval.
		/// </summary>
		[Ignore]
		public TimeSpan Total => (End ?? DateTime.Now) - Start;

		/// <summary>
		/// Start location latitude.
		/// </summary>
		public double StartLatitude { get; set; }

		/// <summary>
		/// Start location longitude.
		/// </summary>
		public double StartLongitude { get; set; }
	}
}