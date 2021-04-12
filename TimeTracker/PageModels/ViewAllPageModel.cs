using TimeTracker.PageModels.Base;
using TimeTracker.Services.Work;

namespace TimeTracker.PageModels
{
	internal class ViewAllPageModel : PageModelBase
	{
		private IWorkService workService;

		public ViewAllPageModel(IWorkService workService) => this.workService = workService;
	}
}