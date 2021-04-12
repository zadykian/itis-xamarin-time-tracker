using System;

namespace TimeTracker.Models
{
	/// <summary>
	/// Tracked time period.
	/// </summary>
	internal class TrackedPeriod
	{
		public TrackedPeriod(Guid userId)
		{
			Id = Guid.NewGuid();
			UserId = userId;
			Start = DateTime.Now;
		}

		public Guid Id { get; }

		public Guid UserId { get; }

		public DateTime Start { get; }

		public DateTime? End { get; set; }

		public TimeSpan Total => (End ?? DateTime.Now) - Start;
	}
}