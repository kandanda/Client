using System;

namespace Kandanda.Ui.Interactivity
{
    public class WindowsOpenUrlRequest: IOpenUrlRequest
    {
        public void Open(Uri url)
        {
            System.Diagnostics.Process.Start(url.ToString());
        }
    }
}
