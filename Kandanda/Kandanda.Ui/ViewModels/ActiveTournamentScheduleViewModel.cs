using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class ActiveTournamentScheduleViewModel : TournamentViewModelBase, IConfirmNavigationRequest
    {
        private readonly ITournamentService _tournamentService;
        public ObservableCollection<Tuple<Phase, List<Match>>> MatchesCollection = new ObservableCollection<Tuple<Phase, List<Match>>>();

        public ActiveTournamentScheduleViewModel(ITournamentService tournamentService)
        {
            Title = "Schedule";
            AutomationId = AutomationIds.ActiveTournamentScheduleTab;
            _tournamentService = tournamentService;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            MatchesCollection.Clear();
            foreach (var phase in _tournamentService.GetPhasesByTournament(CurrentTournament))
            {
                MatchesCollection.Add(new Tuple<Phase, List<Match>>(phase, _tournamentService.GetMatchesByPhase(phase)));
            }
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            throw new NotImplementedException();
        }
    }
}
