using System.Collections.ObjectModel;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentMasterViewModel: TournamentViewModelBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ITournamentService _tournamentService;
        private readonly IEventAggregator _eventAggregator;

        public ObservableCollection<Tournament> Tournaments { get; }

        public ICommand OpenTournamentCommand { get; set; }
        public ICommand CreateTournamentCommand { get; set; }

        public TournamentMasterViewModel(IRegionManager regionManager, ITournamentService tournamentService, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _tournamentService = tournamentService;
            _eventAggregator = eventAggregator;
            CreateTournamentCommand = new DelegateCommand(NavigateToNewTournament);
            OpenTournamentCommand = new DelegateCommand(NavigateToTournament);
            Tournaments = new ObservableCollection<Tournament>(tournamentService.GetAllTournaments());
        }

        private void NavigateToNewTournament()
        {
            CurrentTournament = _tournamentService.CreateEmpty("New Tournament");
            NavigateToTournament();
        }

        private void NavigateToTournament()
        {
            _eventAggregator.GetEvent<ChangeCurrentTournamentEvent>().Publish(CurrentTournament.Id);
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentDetailView");
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Tournaments.Clear();
            var tournaments = await _tournamentService.GetAllTournamentsAsync();
            Tournaments.AddRange(tournaments);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
