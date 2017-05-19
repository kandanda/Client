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
    public class ActiveTournamentViewModel : TournamentViewModelBase, IConfirmNavigationRequest
    {
        private readonly IPublishTournamentService _publishTournamentService;
        private readonly IRegionManager _regionManager;
        private readonly IOpenUrlRequest _openUrlRequest;
        private readonly ITournamentService _tournamentService;
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }
        public InteractionRequest<SignInPopupViewModel> SignInRequest { get; }
        public bool IsReady { get; set; }

        public ActiveTournamentViewModel(IEventAggregator eventAggregator, ITournamentService tournamentService, 
            IPublishTournamentService publishTournamentService, IOpenUrlRequest openUrlRequest, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _tournamentService = tournamentService;
            _publishTournamentService = publishTournamentService;
            _openUrlRequest = openUrlRequest;
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            SignInRequest = new InteractionRequest<SignInPopupViewModel>();
            eventAggregator.GetEvent<TryPublishEvent>().Subscribe(TryPublishAsync);
            eventAggregator.GetEvent<TryUnpublishEvent>().Subscribe(TryUnpublishAsync);
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
            ConfirmationRequest.Raise(
                new Confirmation {Title = $"Save {CurrentTournament.Name}", Content = "Close this Tournament?"},
                c =>
                {
                    if (c.Confirmed)
                    {
                        Save(); 
                    }
                    continuationCallback(c.Confirmed);
                });
        }

        private void Save()
        {
            _tournamentService.Update(CurrentTournament);
        }

        private void TryPublishAsync()
        {
            IsReady = false;
            var signInViewModel = new SignInPopupViewModel(_publishTournamentService);
            SignInRequest.Raise(signInViewModel, async c =>
            {
                if (!c.Confirmed)
                    return;
                await PublishAsync(signInViewModel.AuthToken);
                IsReady = true;
            });
        }
        private async Task PublishAsync(string authToken)
        {
            var response = await _publishTournamentService.PostTournamentAsync(CurrentTournament, authToken, CancellationToken.None);
            _openUrlRequest.Open(response.Link);
        }

        private void TryUnpublishAsync()
        {
            IsReady = false;

            CurrentTournament.IsActive = false;
            _tournamentService.Update(CurrentTournament);

            var signInViewModel = new SignInPopupViewModel(_publishTournamentService);
            SignInRequest.Raise(signInViewModel, async c =>
            {
                if (!c.Confirmed)
                    return;
                await UnPublishAsync(signInViewModel.AuthToken);
                IsReady = true;
            });

            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentDetailView");
        }

        private async Task UnPublishAsync(string authToken)
        {

            var response = await _publishTournamentService.PostTournamentAsync(CurrentTournament, authToken, CancellationToken.None);
            _openUrlRequest.Open(response.Link);
        }


    }
}
