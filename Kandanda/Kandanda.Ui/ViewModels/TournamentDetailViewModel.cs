using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Kandanda.Ui.Interactivity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentDetailViewModel : TournamentViewModelBase, IConfirmNavigationRequest
    {
        private readonly IPublishTournamentService _publishTournamentService;
        private readonly IOpenUrlRequest _openUrlRequest;
        private readonly IRegionManager _regionManager;
        private readonly ITournamentService _tournamentService;
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }
        public ICommand SaveCommand { get; set; }
        public bool IsReady { get; set; }

        public TournamentDetailViewModel(IEventAggregator eventAggregator, ITournamentService tournamentService, 
            IPublishTournamentService publishTournamentService, IOpenUrlRequest openUrlRequest, IRegionManager regionManager)
        {
            _tournamentService = tournamentService;
            _regionManager = regionManager;

            _publishTournamentService = publishTournamentService;
            _openUrlRequest = openUrlRequest;
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            eventAggregator.GetEvent<GeneratePlanEvent>().Subscribe(GeneratePlan);
            SaveCommand = new DelegateCommand(Save);
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
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (navigationContext.Uri.ToString().Equals("/TournamentMasterView"))
            {
                ConfirmationRequest.Raise(
                    new Confirmation { Title = $"Save {CurrentTournament.Name}", Content = "Close this Tournament?" },
                    c =>
                    {
                        if (c.Confirmed)
                        {
                            Save();
                        }
                        continuationCallback(c.Confirmed);
                    });
            }
           
        }

        private void Save()
        {
            _tournamentService.Update(CurrentTournament);
        }

        private void GeneratePlan()
        {
            try
            {
                _tournamentService.GeneratePhase(CurrentTournament);
                CurrentTournament.IsActive = true;
                _tournamentService.Update(CurrentTournament);
                _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/ActiveTournamentView");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        
    }
}
