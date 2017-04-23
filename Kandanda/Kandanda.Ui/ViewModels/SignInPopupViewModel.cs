using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Kandanda.Ui.ViewModels
{
    public class SignInPopupViewModel : BindableBase, IConfirmation, IInteractionRequestAware
    {
        private string _email;
        private readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();
        private readonly IPublishTournamentService _publishTournamentService;
        private bool _isReady = true;
        private string _password = "";
        public DelegateCommand SignInCommand { get; }
        public string AuthToken { get; private set; }
        public string Title { get; set; }
        public object Content { get; set; }
        public bool Confirmed { get; set; }
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        public bool IsReady
        {
            get { return _isReady; }
            private set { SetProperty(ref _isReady, value); }
        }

        public SignInPopupViewModel(IPublishTournamentService publishTournamentService)
        {
            _publishTournamentService = publishTournamentService;
            SignInCommand = new DelegateCommand(async () => await SignIn(), CanTrySignIn)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => IsReady);
            Title = $"Sign in {_publishTournamentService.BaseUri}";
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public void PasswordChanged(string password)
        {
            _password = password;
            SignInCommand.RaiseCanExecuteChanged();
        }

        private async Task SignIn()
        {
            IsReady = false;
            var authToken = await _publishTournamentService.AuthenticateAsync(Email, _password, CancellationToken.None);

            if (authToken != null)
            {
                AuthToken = authToken;
                Confirmed = true;
                FinishInteraction();
            }
            IsReady = true;
        }

        public bool CanTrySignIn()
        {
            return IsReady && _emailAddressAttribute.IsValid(Email) && _password.Length > 3;
        }
    }
}
