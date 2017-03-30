using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace Kandanda.Ui.ViewModels
{
    public class SignInPopupViewModel : BindableBase, IConfirmation, IInteractionRequestAware
    {
        private string _email;
        private string _password;
        private readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();
        private readonly IPublishTournamentService _publishTournamentService;
        public ICommand SignInCommand;

        public SignInPopupViewModel(IPublishTournamentService publishTournamentService)
        {
            _publishTournamentService = publishTournamentService;
            SignInCommand = new DelegateCommand(SignIn, CanTrySignIn)
                .ObservesProperty(() => Email)
                .ObservesProperty(() => Password);
        }

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public string AuthToken { get; private set; }

        public string Title { get; set; }
        public object Content { get; set; }
        public bool Confirmed { get; set; }
        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        private async void SignIn()
        {
            string authToken;
            do
            {
                //authToken = await _publishTournamentService.AuthenticateAsync(Email, Password, CancellationToken.None);
                authToken = "123";
                await Task.Delay(2000);
            } while (authToken == null);
            AuthToken = authToken;
            Confirmed = true;
            FinishInteraction();
        }

        public bool CanTrySignIn()
        {
            return _emailAddressAttribute.IsValid(Email) && Password.Length > 3;
        }
    }
}
