using Microsoft.Practices.Unity;
using Prism.Unity;
using Kandanda.Ui.Views;
using System.Windows;
using Prism.Modularity;

namespace Kandanda.Ui
{
    class Bootstrapper : UnityBootstrapper
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
            var moduleKandanda = typeof(ModuleKandanda);
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = moduleKandanda.Name,
                ModuleType = moduleKandanda.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable,
            });
        }
    }
}
