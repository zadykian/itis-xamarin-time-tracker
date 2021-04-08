using TimeTracker.PageModels.Base;
using TimeTracker.ViewModels.Buttons;

namespace TimeTracker.PageModels
{
    internal class RecentActivityPageModel : PageModelBase
    {

        private ButtonModel viewAllButtonModel;
        public ButtonModel ViewAllButtonModel
        {
            get => viewAllButtonModel;
            set => SetProperty(ref viewAllButtonModel, value);
        }

        private ButtonModel startTimerButtonModel;
        public ButtonModel StartTimerButtonModel
        {
            get => startTimerButtonModel;
            set => SetProperty(ref startTimerButtonModel, value);
        }

        public RecentActivityPageModel()
        {
            ViewAllButtonModel = new ButtonModel("view all", OnViewAll);
            StartTimerButtonModel = new ButtonModel("start timer", OnClockIn);
        }

        private void OnClockIn()
        {

        }

        private void OnViewAll()
        {

        }
    }
}
