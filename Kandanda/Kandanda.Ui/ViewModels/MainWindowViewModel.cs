using System.Windows.Input;
using Kandanda.Ui.Core;
using Kandanda.Ui.Interactivity;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;

namespace Kandanda.Ui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IVersionInfo VersionInfo { get; }
        public InteractionRequest<INotification> ShowAboutRequest { get; }

        //TODO: Fix Dependency Injection
        public MainWindowViewModel(/*IVersionInfo versionInfo*/)
        {
            //VersionInfo = versionInfo;
            //ShowAboutRequest = new InteractionRequest<INotification>();
            Title = "Kandanda Client";
        }

        private void ShowAbout()
        {
            ShowAboutRequest.Raise(new Notification { Title = "About Kandanda", Content = $"Kandanda Version: {VersionInfo.Version}" });
        }
    }
}
