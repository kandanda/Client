using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Events;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentsViewModel : TournamentViewModelBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsViewModel(ITournamentService tournamentService, IEventAggregator eventAggregator)
        {
            Title = "Tournaments";
            AutomationId = AutomationIds.MainViewTournamentsTab;
            _tournamentService = tournamentService;
            eventAggregator.GetEvent<ChangeCurrentTournamentEvent>().Subscribe(PropagateNewTournament);
        }

        private void PropagateNewTournament(int tournamentId)
        {
            var tournament = _tournamentService.GetTournamentById(tournamentId);
            CurrentTournament = tournament;
        }
    }
}
