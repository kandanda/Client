﻿using System.Threading.Tasks;
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
            GeneratePlanCommand = new DelegateCommand(GeneratePlan).ObservesCanExecute(o => IsReady);
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
            _openUrlRequest.Open("https://www.kandanda.ch/");
        }

        private void GoToControlPanel()
        {
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
