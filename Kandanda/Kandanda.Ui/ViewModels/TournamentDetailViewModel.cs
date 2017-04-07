using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
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
        private readonly ITournamentService _tournamentService;
        private int _groupSize;
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }
        public InteractionRequest<SignInPopupViewModel> SignInRequest { get; }
        public ICommand SaveCommand { get; set; }
        public bool IsReady { get; set; }

        public TournamentDetailViewModel(IEventAggregator eventAggregator, ITournamentService tournamentService, 
            IPublishTournamentService publishTournamentService, IOpenUrlRequest openUrlRequest)
        {
            _tournamentService = tournamentService;
            _publishTournamentService = publishTournamentService;
            _openUrlRequest = openUrlRequest;
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            SignInRequest = new InteractionRequest<SignInPopupViewModel>();
            eventAggregator.GetEvent<PublishRequestEvent>().Subscribe(SignInAsync);
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

        private void SignInAsync()
        {
            IsReady = false;
            GeneratePlanAsync();
            var signInViewModel = new SignInPopupViewModel(_publishTournamentService);
            SignInRequest.Raise(signInViewModel, async c =>
            {
                if (!c.Confirmed)
                    return;
                await Publish(signInViewModel.AuthToken);
                IsReady = true;
            });
        }

        private async Task Publish(string authToken)
        {
            var response = await _publishTournamentService.PostTournamentAsync(CurrentTournament, authToken, CancellationToken.None);
            _openUrlRequest.Open(response.Link);
        }

        private void GeneratePlanAsync()
        {
            // TODO: Fix Generate Plan
            //_tournamentService.GeneratePhase(CurrentTournament, 4);
        }
    }
}
