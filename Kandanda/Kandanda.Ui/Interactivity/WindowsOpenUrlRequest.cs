using System;

namespace Kandanda.Ui.Interactivity
{
    public class WindowsOpenUrlRequest: IOpenUrlRequest
    {
        public void Open(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
