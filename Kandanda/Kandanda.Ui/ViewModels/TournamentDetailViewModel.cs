using System;
using System.Threading;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Kandanda.Ui.Events;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentDetailViewModel : ViewModelBase, IConfirmNavigationRequest
    {
        private readonly IPublishTournamentService _publishTournamentService;
        private readonly IEventAggregator _eventAggregator;
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }
        public InteractionRequest<SignInPopupViewModel> SignInRequest { get; }
        public bool IsReady { get; set; }

        public TournamentDetailViewModel(IEventAggregator eventAggregator, IPublishTournamentService publishTournamentService)
        {
            _eventAggregator = eventAggregator;
            _publishTournamentService = publishTournamentService;
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            SignInRequest = new InteractionRequest<SignInPopupViewModel>();
            eventAggregator.GetEvent<GeneratePlanRequestEvent>().Subscribe(GeneratePlanAsync);
            eventAggregator.GetEvent<PublishRequestEvent>().Subscribe(SignInAsync);
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
                new Confirmation {Title = "Kandanda", Content = "Are you sure you want to close this Tournament?"}, c => continuationCallback(c.Confirmed));
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
