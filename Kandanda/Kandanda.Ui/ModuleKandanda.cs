using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
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
                .RegisterType<IOpenUrlRequest, WindowsOpenUrlRequest>()
                .RegisterType<ITournamentService, TournamentService>();
            _container
                .RegisterTypeForNavigation<TournamentsMasterView>()
                .RegisterTypeForNavigation<TournamentsDetailView>()
                .RegisterTypeForNavigation<TournamentInfoView>()
                .RegisterTypeForNavigation<SheduleView>()
                .RegisterTypeForNavigation<ParticipantView>();
            _regionManager
                .RegisterViewWithRegion(RegionNames.MainRegion, typeof(TournamentsView))
                .RegisterViewWithRegion(RegionNames.TournamentsRegion, typeof(TournamentsMasterView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(TournamentInfoView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(SheduleView))
                .RegisterViewWithRegion(RegionNames.TournamentDetailRegion, typeof(ParticipantView))
                .RegisterViewWithRegion(RegionNames.TournamentCommandRegion, typeof(TournamentCommandView))
                .RegisterViewWithRegion(RegionNames.MainRegion, typeof(TeamsView))
                .RegisterViewWithRegion(RegionNames.MenuRegion, typeof(Menubar));
        }
    }
}