using Prism.Commands;
using System.Windows.Input;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private string _name = "Tournament1";
        private int _numberOfGroups = 4;
        public ICommand ContinueCommand { get; set; }

        public TournamentViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Tournament";
            ContinueCommand = new DelegateCommand(ContinueToNextStep, CanContinue)
                .ObservesProperty(() => Name)
                .ObservesProperty(() => NumberOfGroups);
        }

        private void ContinueToNextStep()
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "/ParticipantView");
        }

        private bool CanContinue()
        {
            return !string.IsNullOrWhiteSpace(Name) && NumberOfGroups > 1;
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public int NumberOfGroups
        {
            get { return _numberOfGroups; }
            set { SetProperty(ref _numberOfGroups, value); }
        }
    }
}
