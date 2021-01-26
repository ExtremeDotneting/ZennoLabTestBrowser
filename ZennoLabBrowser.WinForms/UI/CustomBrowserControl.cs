using System;
using System.Diagnostics;
using IRO.XWebView.CefSharp;
using IRO.XWebView.Core;
using ZennoLabBrowser.WinForms.Models;
using ZennoLabBrowser.WinForms.Services;

namespace ZennoLabBrowser.WinForms.UI
{
    public partial class CustomBrowserControl : DevExpress.XtraEditors.XtraUserControl
    {
        public CefSharpXWebView XWV { get; }

        public event Action<IXWebView, string> PageTitleChanged;

        public CustomBrowserControl()
        {
            InitializeComponent();
            XWV = new CefSharpXWebView(CurrentXWebViewControl, new BrowserRequestHandler());

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
            XWV.LoadFinished += async (s, e) =>
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
                    try
                    {
                        var title = await XWV.ExJs<string>("return document.title;");
                        PageTitleChanged?.Invoke(XWV, title);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            };
            XWV.Disposing += delegate
            {
                StatusMessagesService.Write("Closing WebView.");
            };
            //-----

            XWV.WaitInitialization().ContinueWith((t) =>
            {
                XWV.LoadUrl(GlobalObjects.UserSettings.HomeUrl);
            });
           

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
