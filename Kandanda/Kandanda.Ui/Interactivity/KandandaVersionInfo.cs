using System;
using System.Reflection;

namespace Kandanda.Ui.Interactivity
{
    public class KandandaVersionInfo: IVersionInfo
    {
        private readonly Lazy<string> _version = new Lazy<string>(() => Assembly.GetEntryAssembly().GetName().Version.ToString());
        public string Version => _version.Value;
    }
}
