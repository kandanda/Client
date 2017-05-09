using Prism.Commands;
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
        public ICommand PublishCommand { get; }

        public TournamentCommandViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            CloseCommand = new DelegateCommand(Close);
            PublishCommand = new DelegateCommand(Publish);
        }

        private void Close()
        {
            _regionManager.RequestNavigate(RegionNames.TournamentsRegion, "/TournamentMasterView");
        }

        private void Publish()
        {
            _eventAggregator.GetEvent<PublishRequestEvent>().Publish();
        }
    }
}
