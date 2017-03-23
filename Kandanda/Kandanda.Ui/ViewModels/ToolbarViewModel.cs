using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.Ui.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Kandanda.Ui.ViewModels
{
    public class ToolbarViewModel : BindableBase
    {
        private bool _isReady = true;
        private readonly IEventAggregator _eventAggregator;

        public ToolbarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            GeneratePlanCommand = new DelegateCommand(GeneratePlan).ObservesCanExecute(o => IsReady);
        }

        public ICommand GeneratePlanCommand { get; set; }

        public bool IsReady
        {
            get { return _isReady; }
            set { SetProperty(ref _isReady, value); }
        }

        private async void GeneratePlan()
        {
            var stateChangeEvent = _eventAggregator.GetEvent<StateChangeEvent>();
            stateChangeEvent.Publish("Generating Plan ...");
            IsReady = false;
            await Task.Delay(3000);
            stateChangeEvent.Publish("Plan generated");
            IsReady = true;
        }
    }
}
