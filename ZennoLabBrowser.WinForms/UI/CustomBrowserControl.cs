using System;
using IRO.XWebView.CefSharp;
using ZennoLabBrowser.WinForms.Models;
using ZennoLabBrowser.WinForms.Services;

namespace ZennoLabBrowser.WinForms.UI
{
    public partial class CustomBrowserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public CefSharpXWebView XWV { get; }

        public CustomBrowserControl()
        {
            InitializeComponent();
            XWV = new CefSharpXWebView(CurrentXWebViewControl);

            //Register events.
            //-----
            XWV.LoadStarted += (s, e) =>
            {
                if (!e.Cancel)
                    StatusMessagesService.Write($"Navigating to '{e.Url}'. ");
            };
            XWV.GoBackRequested += (s, e) =>
            {
                StatusMessagesService.Write($"Go back'.");
            };
            XWV.GoForwardRequested += (s, e) =>
            {
                StatusMessagesService.Write($"Go forward'.");
            };
            XWV.LoadFinished += (s, e) =>
            {
                XWV.ThreadSync.Invoke(() =>
                {
                    UrlTextEdit.Text = e.Url;
                });
                if (e.IsError)
                {
                    StatusMessagesService.Write($"Error while loading '{e.Url}'. " +
                                                $"Error description: '{e.ErrorDescription}'.");
                }
                else
                {
                    StatusMessagesService.Write($"Loaded '{e.Url}'. ");
                }
            };
            XWV.Disposing += delegate
            {
                StatusMessagesService.Write("Closing WebView.");
            };
            //-----

            XWV.LoadUrl(UserSettings.Inst.HomeUrl);
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            XWV.GoBack();
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            XWV.GoForward();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            StatusMessagesService.Write("Refreshing.");
            XWV.Reload();
        }

        private void NavigateUrlButton_Click(object sender, EventArgs e)
        {
            XWV.LoadUrl(UrlTextEdit.Text);
        }
    }
}
