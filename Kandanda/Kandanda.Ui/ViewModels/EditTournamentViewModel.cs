using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Kandanda.Ui.Core;
using Kandanda.Ui.Views;
using Microsoft.Practices.Unity;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class EditTournamentViewModel : ViewModelBase, IConfirmNavigationRequest
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IPublishTournamentService _publishTournamentService;
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; }
        public InteractionRequest<SignInPopupViewModel> SignInRequest { get; }

        public EditTournamentViewModel(IUnityContainer unityContainer, IPublishTournamentService publishTournamentService)
        {
            _unityContainer = unityContainer;
            _publishTournamentService = publishTournamentService;
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            SignInRequest = new InteractionRequest<SignInPopupViewModel>();
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

        public async void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            var confirmation = await ConfirmationRequest.RaiseAsync(
                new Confirmation {Title = "Kandanda", Content = "Are you sure you want to close this Tournament?"});
            continuationCallback(confirmation.Confirmed);
        }

        private async void SignIn()
        {
            var signInViewModel = _unityContainer.Resolve<SignInPopupViewModel>();
            await SignInRequest.RaiseAsync(signInViewModel);
            if (signInViewModel.Confirmed)
            {
                await _publishTournamentService.PostTournamentAsync(new Tournament(), signInViewModel.AuthToken, CancellationToken.None);
            }
        }
    }
}
