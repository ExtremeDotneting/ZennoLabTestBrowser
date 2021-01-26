using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Handler;
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

    public class CustomResourceRequestHandler : ResourceRequestHandler
    {
        readonly HttpClient _httpClient = new HttpClient();


        protected override IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            var resType = request.ResourceType;
            var isTextResourceType = resType != ResourceType.Image
                                     && resType != ResourceType.Media
                                     && resType != ResourceType.Favicon;
            if (isTextResourceType)
            {
                var resp = SendResourceRequest(request).Result;
                if (resp.Content.Headers.ContentType.MediaType == "application/json")
                {
                    return new FromHttpResponseMessage_ResourceHandler(resp);
                }
            }

            return base.GetResourceHandler(chromiumWebBrowser, browser, frame, request);
        }

        async Task<HttpResponseMessage> SendResourceRequest(IRequest request)
        {
            if (request.PostData == null || request.PostData.Elements.Count == 0)
                return null;
            var bytes = request.PostData.Elements[0].Bytes;
            var requestPostDataStr = Encoding.UTF8.GetString(bytes);
            requestPostDataStr = requestPostDataStr.Replace("AAAAAAAA", "BBBBBBBBB");
            request.PostData.Elements[0].Bytes = Encoding.UTF8.GetBytes(requestPostDataStr);
            var method = HttpClientExt.HttpMethodFromString(request.Method);
            var url = request.Url;
            var reqMsg = new HttpRequestMessage(method, url);
            var resp = await _httpClient.SendAsync(reqMsg);
            return resp;
        }
    }

    public class FromHttpResponseMessage_ResourceHandler : ResourceHandler
    {
        readonly HttpResponseMessage _response;

        public FromHttpResponseMessage_ResourceHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        public override CefReturnValue ProcessRequestAsync(IRequest request, ICallback callback)
        {
            Task.Run(async () =>
            {
                var contentStr = await _response.Content.ReadAsStringAsync();
                MimeType = _response.Content.Headers.ContentType.MediaType;
                Stream = GenerateStreamFromString(contentStr);
                StatusCode = (int)_response.StatusCode;
                foreach (var header in _response.Headers)
                {
                    Headers[header.Key] = string.Join(", ", header.Value);
                }

                //var stream =await resp.Content.ReadAsStreamAsync();
                //stream.Position = 0;

                callback.Continue();
            });

            return CefReturnValue.ContinueAsync;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }


    }
}
