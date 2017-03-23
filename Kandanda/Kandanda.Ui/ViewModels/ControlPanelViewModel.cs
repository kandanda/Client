using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        public ICommand CreateTournamentCommand { get; set; }

        public ControlPanelViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            CreateTournamentCommand = new DelegateCommand(NavigateToNewTournament);
        }

        private void NavigateToNewTournament()
        {
            _regionManager.RequestNavigate(RegionNames.WindowsRegion, "EditTournamentView");
        }
    }
}
