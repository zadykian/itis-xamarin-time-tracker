using System;
using Xamarin.Essentials;

namespace TimeTracker.Models
{
	/// <summary>
	/// Tracked time period.
	/// </summary>
	internal class TrackedPeriod
	{
		public TrackedPeriod(Guid userId, Location startLocation)
		{
			Id = Guid.NewGuid();
			UserId = userId;
			Start = DateTime.Now;
			StartLocation = startLocation;
		}

		/// <summary>
		/// Period's id.
		/// </summary>
		public Guid Id { get; }

		/// <summary>
		/// Id of a user which this period belongs to.
		/// </summary>
		public Guid UserId { get; }

		/// <summary>
		/// Period's start.
		/// </summary>
		public DateTime Start { get; }

		/// <summary>
		/// Period's end.
		/// </summary>
		public DateTime? End { get; set; }

		/// <summary>
		/// Period's total interval.
		/// </summary>
		public TimeSpan Total => (End ?? DateTime.Now) - Start;

		/// <summary>
		/// Period's start location.
		/// </summary>
		public Location StartLocation { get; }
	}
}