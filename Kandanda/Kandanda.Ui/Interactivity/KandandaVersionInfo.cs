using System.Reflection;

namespace Kandanda.Ui.Interactivity
{
    class KandandaVersionInfo: IVersionInfo
    {
        public string Version => Assembly.GetEntryAssembly().GetName().Version.ToString();
    }
}
