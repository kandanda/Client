using System;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using TestStack.White;

namespace Kandanda.Specs
{
    [Binding]
    public sealed class Hooks
    {
        public static string BaseDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string SystemUnderTest => Path.Combine(BaseDir, "Kandanda.Ui.exe");
        private static Application _application;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Console.WriteLine("Launch Application...");
            _application = Application.Launch(SystemUnderTest);
            FeatureContext.Current.Set(_application, "app");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
           _application.Close();
        }
    }
}
