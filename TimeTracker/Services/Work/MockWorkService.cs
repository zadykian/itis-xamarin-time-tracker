using System.Collections.Generic;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Work
{
	internal class MockWorkService : IWorkService
	{
		public List<WorkItem> Items { get; }

		public MockWorkService() => Items = new List<WorkItem>();

		public Task<bool> LogWorkAsync(WorkItem item)
		{
			Items.Add(item);
			return Task.FromResult(true);
		}
	}
}