﻿using System;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentSheduleViewModel : TournamentViewModelBase, INavigationAware
    {
        private readonly ITournamentService _tournamentService;
        

        public TournamentSheduleViewModel(ITournamentService tournamentService)
        {
            Title = "Schedule";
            AutomationId = AutomationIds.TournamentScheduleTab;
            _tournamentService = tournamentService;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Save();
        }
        
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void Save()
        {
           _tournamentService.Update(CurrentTournament);
        }
    }
}
