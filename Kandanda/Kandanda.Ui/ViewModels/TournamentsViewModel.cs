using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentsViewModel : ViewModelBase
    {

        private readonly IRegionManager _regionManager;
        private readonly ITournamentService _tournamentService;
        public ObservableCollection<Tournament> Tournaments { get; }

        public ICommand CreateTournamentCommand { get; }
        public ICommand OpenTournamentCommand { get; }
        
        public TournamentsViewModel(IRegionManager regionManager, ITournamentService tournamentService)
        {
            Title = "Tournaments";
            _regionManager = regionManager;
            _tournamentService = tournamentService;
            CreateTournamentCommand = new DelegateCommand(NavigateToNewTournament);
            OpenTournamentCommand = new DelegateCommand(NavigateToOpenTournament);
            Tournaments = new ObservableCollection<Tournament>(tournamentService.GetAllTournaments());
        }

        private void NavigateToNewTournament()
        {
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentDetailView");
        }

        private void NavigateToOpenTournament()
        {
            var navigationParameters = new NavigationParameters { {"Tournament", 2 } };
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentDetailView", navigationParameters);
        }
    }
}
