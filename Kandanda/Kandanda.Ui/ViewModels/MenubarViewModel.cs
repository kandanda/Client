using System.Threading.Tasks;
using System.Windows.Input;
using Kandanda.Ui.Events;
using Kandanda.Ui.Interactivity;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class MenubarViewModel : BindableBase
    {
        private bool _isReady = true;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IVersionInfo _versionInfo;
        private readonly IOpenUrlRequest _openUrlRequest;
        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public InteractionRequest<IConfirmation> ConfirmationRequest { get; set; }
        public ICommand GeneratePlanCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand ShowAboutCommand { get; set; }
        public ICommand OpenDocumentationCommand { get; set; }

        public MenubarViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IVersionInfo versionInfo, IOpenUrlRequest openUrlRequest)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _versionInfo = versionInfo;
            _openUrlRequest = openUrlRequest;
            GeneratePlanCommand = new DelegateCommand(GeneratePlan).ObservesCanExecute(o => IsReady);
            ShowAboutCommand = new DelegateCommand(ShowAbout);
            OpenDocumentationCommand = new DelegateCommand(OpenDocumentation);
            CloseCommand = new DelegateCommand(Close);
            NotificationRequest = new InteractionRequest<INotification>();
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
        }

        public bool IsReady
        {
            get { return _isReady; }
            set { SetProperty(ref _isReady, value); }
        }

        private void ShowAbout()
        {
            NotificationRequest.Raise(new Notification {Title = "About Kandanda", Content = $"Kandanda Version: {_versionInfo.Version}"});
        }

        private void OpenDocumentation()
        {
            _openUrlRequest.Open("https://www.kandanda.ch/");
        }

        private async void Close()
        {
            var notificatation = await ConfirmationRequest.RaiseAsync(new Confirmation {Title = "Kandanda", Content = "Are you sure you want to close this Tournament?"});
            if (notificatation.Confirmed)
                _regionManager.RequestNavigate(RegionNames.WindowsRegion, "/ControlPanelView");
        }

        private async void GeneratePlan()
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
