﻿using System;

namespace TimeTracker.Models
{
	public class WorkItem
	{
		public DateTime Start { get; set; }

		public DateTime End { get; set; }

		public TimeSpan Total => End - Start;
	}
}