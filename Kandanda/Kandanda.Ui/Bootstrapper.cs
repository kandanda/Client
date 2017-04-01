using Microsoft.Practices.Unity;
using Prism.Unity;
using Kandanda.Ui.Views;
using System.Windows;
using Kandanda.BusinessLayer;
using Prism.Modularity;

namespace Kandanda.Ui
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            var moduleKandandaService = typeof(ModuleKandandaServices);
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = moduleKandandaService.Name,
                ModuleType = moduleKandandaService.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });

            var moduleKandanda = typeof(ModuleKandanda);
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = moduleKandanda.Name,
                ModuleType = moduleKandanda.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }
    }
}
