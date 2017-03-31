using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TournamentCommandViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            CloseCommand = new DelegateCommand(Close);
        }

        private void Close()
        {
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentMasterView");
        }
    }
}
