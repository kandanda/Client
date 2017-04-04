using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Events;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentsViewModel : TournamentViewModelBase
    {

        private readonly IRegionManager _regionManager;
        private readonly ITournamentService _tournamentService;
        private readonly IEventAggregator _eventAggregator;

        public TournamentsViewModel(IRegionManager regionManager, ITournamentService tournamentService, IEventAggregator eventAggregator)
        {
            Title = "Tournaments";
            _regionManager = regionManager;
            _tournamentService = tournamentService;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<ChangeCurrentTournamentEvent>().Subscribe(PropagateNewTournament);
        }

        private void PropagateNewTournament(int tournamentId)
        {
            var tournament = _tournamentService.GetTournamentById(tournamentId);
            CurrentTournament = tournament;
        }
    }
}
