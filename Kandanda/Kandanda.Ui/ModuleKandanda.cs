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
                .RegisterTypeForNavigation<TournamentView>()
                .RegisterTypeForNavigation<ParticipantView>();
            _regionManager
                .RegisterViewWithRegion(RegionNames.ContentRegion, typeof(TournamentView))
                .RegisterViewWithRegion(RegionNames.ContentRegion, typeof(SheduleView))
                .RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ParticipantView))
                .RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(Toolbar))
                .RegisterViewWithRegion(RegionNames.StatusbarRegion, typeof(Statusbar));
        }
    }
}
