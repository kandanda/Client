using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentDetailViewModel : TournamentViewModelBase, IConfirmNavigationRequest
    {
        private readonly IPublishTournamentService _publishTournamentService;
        private readonly IEventAggregator _eventAggregator;
        private readonly ITournamentService _tournamentService;
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }
        public InteractionRequest<SignInPopupViewModel> SignInRequest { get; }
        public ICommand SaveCommand { get; set; }
        public bool IsReady { get; set; }

        public TournamentDetailViewModel(IEventAggregator eventAggregator, ITournamentService tournamentService, 
            IPublishTournamentService publishTournamentService)
        {
            _eventAggregator = eventAggregator;
            _tournamentService = tournamentService;
            _publishTournamentService = publishTournamentService;
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            SignInRequest = new InteractionRequest<SignInPopupViewModel>();
            eventAggregator.GetEvent<GeneratePlanRequestEvent>().Subscribe(GeneratePlanAsync);
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
            var signInViewModel = new SignInPopupViewModel(_publishTournamentService);
            SignInRequest.Raise(signInViewModel, c =>
            {
                if (c.Confirmed)
                    _publishTournamentService.PostTournamentAsync(new Tournament(), signInViewModel.AuthToken, CancellationToken.None);
            });
        }

        private async void GeneratePlanAsync()
        {
            var stateChangeEvent = _eventAggregator.GetEvent<StateChangeEvent>();
            stateChangeEvent.Publish("Generating Plan ...");
            IsReady = false;
            await Task.Delay(3000);
            stateChangeEvent.Publish("Plan generated");
            IsReady = true;
        }
    }
}
