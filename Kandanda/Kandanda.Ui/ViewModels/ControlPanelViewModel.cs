﻿using System.Collections.Generic;
using Prism.Commands;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class ControlPanelViewModel : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ITournamentService _tournamentService;
        public ICommand CreateTournamentCommand { get; }
        public List<Tournament> Tournaments { get; set; }
        
        public ControlPanelViewModel(IRegionManager regionManager, ITournamentService tournamentService)
        {
            _regionManager = regionManager;
            _tournamentService = tournamentService;
            CreateTournamentCommand = new DelegateCommand(NavigateToNewTournament);
            Tournaments = tournamentService.GetAllTournaments();
        }

        private void NavigateToNewTournament()
        {
            _regionManager.RequestNavigate(RegionNames.WindowsRegion, "/EditTournamentView");
        }
    }
}
