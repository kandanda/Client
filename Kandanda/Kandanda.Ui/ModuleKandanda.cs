using Kandanda.Ui.Interactivity;
using Kandanda.Ui.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace Kandanda.Ui
{
    public class ModuleKandanda : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ModuleKandanda(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _container
                .RegisterType<IVersionInfo, KandandaVersionInfo>()
                .RegisterType<IOpenUrlRequest, WindowsOpenUrlRequest>();
            _container
                .RegisterTypeForNavigation<TournamentMasterView>()
                .RegisterTypeForNavigation<TournamentDetailView>()
                .RegisterTypeForNavigation<TournamentInformationView>()
                .RegisterTypeForNavigation<TournamentSheduleView>()
                .RegisterTypeForNavigation<TournamentParticipantView>()
                .RegisterTypeForNavigation<ActiveTournamentView>()
                .RegisterTypeForNavigation<ActiveTournamentScheduleView>();
            _regionManager
                .RegisterViewWithRegion(RegionNames.MainRegion, typeof(TournamentsView))
                .RegisterViewWithRegion(RegionNames.TournamentsRegion, typeof(TournamentMasterView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(TournamentInformationView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(TournamentSheduleView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(TournamentParticipantView))
                .RegisterViewWithRegion(RegionNames.TournamentCommandRegion, typeof(TournamentCommandView))
                .RegisterViewWithRegion(RegionNames.ActiveTournamentRegion, typeof(ActiveTournamentScheduleView))
                .RegisterViewWithRegion(RegionNames.ActiveTournamentCommandRegion, typeof(ActiveTournamentCommandView))
                .RegisterViewWithRegion(RegionNames.MainRegion, typeof(TeamsView))
                .RegisterViewWithRegion(RegionNames.MenuRegion, typeof(Menubar))
                .RegisterViewWithRegion(RegionNames.StatusbarRegion, typeof(Statusbar));
        }
    }
}