using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Commands;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class ParticipantViewModel: ViewModelBase
    {
        private bool _isGenerating;
        private readonly IEventAggregator _eventAggregator;

        public ParticipantViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Title = "Participants";
            GeneratePlanCommand = new DelegateCommand(GeneratePlan, CanGenerate)
                .ObservesProperty(() => IsGenerating);
        }
        public ICommand GeneratePlanCommand { get; set; }

        public bool IsGenerating
        {
            get { return _isGenerating; }
            set { SetProperty(ref _isGenerating, value); }
        }

        private async void GeneratePlan()
        {
            var stateChangeEvent = _eventAggregator.GetEvent<StateChangeEvent>();
            stateChangeEvent.Publish("Generating Plan ...");
            IsGenerating = true;
            await Task.Delay(3000);
            stateChangeEvent.Publish("Plan generated");
            IsGenerating = false;
        }

        private bool CanGenerate()
        {
            return !_isGenerating;
        }
    }
}
