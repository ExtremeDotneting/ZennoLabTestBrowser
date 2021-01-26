using System;
using System.Collections.Generic;
using System.Linq;
using CefSharp;
using IRO.XWebView.CefSharp.BrowserClients;

namespace ZennoLabBrowser.WinForms.Services
{
    public class BrowserRequestHandler : CustomRequestHandler
    {
        readonly CustomResourceRequestHandler _customResourceRequestHandler = new CustomResourceRequestHandler();

        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return _customResourceRequestHandler;
        }
    }
}
