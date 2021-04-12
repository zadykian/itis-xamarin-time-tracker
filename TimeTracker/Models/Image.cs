using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTracker.Models
{
	/// <summary>
	/// Image taken during time tracking.
	/// </summary>
	internal class Image
	{
		public Image(IEnumerable<byte> content, Guid trackedPeriodId)
		{
			Content = content.ToArray();
			TrackedPeriodId = trackedPeriodId;
		}

		/// <summary>
		/// Image's content.
		/// </summary>
		public IReadOnlyCollection<byte> Content { get; }

		/// <summary>
		/// Tracked period which this photo belongs to.
		/// </summary>
		public Guid TrackedPeriodId { get; }
	}
}