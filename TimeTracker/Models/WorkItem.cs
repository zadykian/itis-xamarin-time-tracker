using System;
using TimeTracker.Services;

namespace TimeTracker.Models
{
    public class WorkItem : IIdentifiable
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Total => End - Start;

        public string Id { get; set; }
    }
}
