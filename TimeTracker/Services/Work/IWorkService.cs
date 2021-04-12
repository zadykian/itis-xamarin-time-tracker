using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Work
{
	internal interface IWorkService
	{
		Task<bool> LogWorkAsync(WorkItem item);
	}
}