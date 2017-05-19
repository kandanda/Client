using System;
using System.Windows;
using Prism.Logging;

namespace Kandanda.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILoggerFacade _logger;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
            _logger = bootstrapper.GetLogger();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Log($"{e} {e.ExceptionObject}", Category.Exception, Priority.High);
        }
    }
}
