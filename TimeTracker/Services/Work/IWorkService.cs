using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Work
{
	internal interface IWorkService
	{
		Task<bool> LogWorkAsync(WorkItem item);

		Task<ObservableCollection<WorkItem>> GetTodaysWorkAsync();

		Task<List<WorkItem>> GetWorkForThisPeriodAsync();
	}
}