using System.Collections.Generic;
using System.Linq;
using SQLite;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace TimeTracker.Models
{
	/// <summary>
	/// Image taken during time tracking.
	/// </summary>
	internal class Image
	{
		public Image()
		{
		}
		
		public Image(IEnumerable<byte> content, int trackedPeriodId)
		{
			Content = content.ToArray();
			TrackedPeriodId = trackedPeriodId;
		}

		/// <summary>
		/// Image's content.
		/// </summary>
		public byte[] Content { get; set; }

		/// <summary>
		/// Tracked period which this photo belongs to.
		/// </summary>
		[Indexed]
		public int TrackedPeriodId { get; set; }
	}
}