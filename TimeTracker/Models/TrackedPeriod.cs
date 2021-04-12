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

		public Guid Id { get; }

		public Guid UserId { get; }

		public DateTime Start { get; }

		public DateTime? End { get; set; }

		public TimeSpan Total => (End ?? DateTime.Now) - Start;

		public Location StartLocation { get; }
	}
}