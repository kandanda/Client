using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using TestStack.White;

namespace Kandanda.Ui.Testing
{
    [Binding]
    public sealed class Hooks
    {
        public static string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string SystemUnderTest => Path.Combine(BaseDir, "..\\..\\..\\Kandanda.Ui\\bin\\Debug\\Kandanda.Ui.exe");
                
        private static Application _application;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            _application = Application.Launch(SystemUnderTest);
            FeatureContext.Current.Set(_application, "app");
        }

        [AfterScenario]
        public static void AfterScenario()
        {
           _application.Close();
        }
    }
}
