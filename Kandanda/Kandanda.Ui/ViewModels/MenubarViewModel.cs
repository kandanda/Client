using System;
using System.Windows.Input;
using Kandanda.Ui.Events;
using Kandanda.Ui.Interactivity;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class MenubarViewModel : BindableBase
    {
        private bool _isReady = true;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IOpenUrlRequest _openUrlRequest;
        public ICommand GeneratePlanCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand OpenDocumentationCommand { get; }
        public ICommand ShowAboutCommand { get; set; }

        public MenubarViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, IOpenUrlRequest openUrlRequest)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _openUrlRequest = openUrlRequest;
            GeneratePlanCommand = new DelegateCommand(RequestGeneratePlan).ObservesCanExecute(() => IsReady);
            OpenDocumentationCommand = new DelegateCommand(OpenDocumentation);
            CloseCommand = new DelegateCommand(GoToControlPanel);
            ShowAboutCommand = new DelegateCommand(ShowAboutRequest);
        }

        private void ShowAboutRequest()
        {
            //TODO : implement Show About Request
        }

        public bool IsReady
        {
            get { return _isReady; }
            set { SetProperty(ref _isReady, value); }
        }

        private void OpenDocumentation()
        {
            _openUrlRequest.Open(new Uri("https://www.kandanda.ch/"));
        }

        private void GoToControlPanel()
        {
            _regionManager.RequestNavigate(RegionNames.MainRegion, "/ControlPanelView");
        }

        private void RequestGeneratePlan()
        {
            _eventAggregator.GetEvent<PublishRequestEvent>().Publish();
        }
    }
}
