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
                .RegisterTypeForNavigation<TournamentInfoView>()
                .RegisterTypeForNavigation<ScheduleView>()
                .RegisterTypeForNavigation<TournamentParticipantView>();
            _regionManager
                .RegisterViewWithRegion(RegionNames.MainRegion, typeof(TournamentsView))
                .RegisterViewWithRegion(RegionNames.TournamentsRegion, typeof(TournamentMasterView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(TournamentInfoView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(ScheduleView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(TournamentParticipantView))
                .RegisterViewWithRegion(RegionNames.TournamentCommandRegion, typeof(TournamentCommandView))
                .RegisterViewWithRegion(RegionNames.MainRegion, typeof(ParticipantsView))
                .RegisterViewWithRegion(RegionNames.MenuRegion, typeof(Menubar));
        }
    }
}