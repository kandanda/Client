using System;
using Prism.Mvvm;
using Kandanda.Ui.Events;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class StatusbarViewModel : BindableBase
    {
        private string _applicationState = "Ready";
        private string _kandandaDbContextInformation;

        public StatusbarViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<StateChangeEvent>().Subscribe(UpdateState);
            eventAggregator.GetEvent<KandandaDbContextChanged>().Subscribe(UpdateKandandaDbContextInformation);
        }

        private void UpdateState(string state)
        {
            ApplicationState = state;
        }

        private void UpdateKandandaDbContextInformation()
        {
            KandandaDbContextInformation = "Reset DB on " + DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        public string ApplicationState
        {
            get { return _applicationState; }
            set { SetProperty(ref _applicationState, value); }
        }

        public string KandandaDbContextInformation
        {
            get { return _kandandaDbContextInformation; }
            set { SetProperty(ref _kandandaDbContextInformation, value); }
        }
    }
}
