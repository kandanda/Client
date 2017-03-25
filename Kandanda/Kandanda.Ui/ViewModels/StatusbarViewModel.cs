using Prism.Mvvm;
using Kandanda.Ui.Events;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class StatusbarViewModel : BindableBase
    {
        private string _applicationState = "Ready";

        public StatusbarViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<StateChangeEvent>().Subscribe(UpdateState);
        }

        private void UpdateState(string state)
        {
            ApplicationState = state;
        }

        public string ApplicationState
        {
            get { return _applicationState; }
            set { SetProperty(ref _applicationState, value); }
        }
    }
}
