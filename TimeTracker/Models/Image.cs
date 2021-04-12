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
			TrackedPeriodId = trackedPeriodId;
			Content = content.ToArray();
		}

		public IReadOnlyCollection<byte> Content { get; }

		public Guid TrackedPeriodId { get; }
	}
}