using System.Threading.Tasks;
using IRO.XWebView.Core.Utils;

namespace ZennoLabBrowser.WinForms.Services
{
    public static class StatusMessagesService
    {
        public static void Write(string str)
        {
            XWebViewThreadSync.Inst.TryInvokeAsync(() =>
            {
                var statusStrip = GlobalObjects.CurrentStatusStripLabel;
                if (statusStrip == null || statusStrip.IsDisposed)
                    return;
                statusStrip.Text = str;
            });
        }
        
        public static void Clear()
        {
            Write("");
        }

        public static void StartAutoClearThread()
        {
            //Fast crunch implemention.
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(5000);
                    Clear();
                }
            });
        }
    }
}
