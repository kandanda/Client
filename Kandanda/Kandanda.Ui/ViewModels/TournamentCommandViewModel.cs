﻿using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using Kandanda.Ui.Events;
using Prism.Events;
using Prism.Regions;

namespace Kandanda.Ui.ViewModels
{
    public class TournamentCommandViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public ICommand CloseCommand { get; }
        public ICommand GenerateCommand { get; }

        public TournamentCommandViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            CloseCommand = new DelegateCommand(Close);
            GenerateCommand = new DelegateCommand(Generate);
        }

        private void Close()
        {
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentMasterView");
        }

        private void Generate()
        {
            _eventAggregator.GetEvent<GeneratePlanEvent>().Publish();
        }
    }
}
