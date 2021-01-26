using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Handler;

namespace ZennoLabBrowser.WinForms.Services
{
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

            requestPostDataStr = requestPostDataStr.Replace("AAAAAAAA", "BBBBBBBBB");

            var method = HttpClientExt.HttpMethodFromString(request.Method);
            var url = request.Url;
            var reqMsg = new HttpRequestMessage(method, url);
            var resp = await _httpClient.SendAsync(reqMsg);
            return resp;
        }


    }

    public class AfterResourceRequestEventArgs : EventArgs
    {
        readonly IRequest _request;

        public AfterResourceRequestEventArgs(IRequest request, bool usedManagedRequestHandler)
        {
            _request = request;
            UsedManagedRequestHandler = usedManagedRequestHandler;
            ResType = request.ResourceType;
            Url = request.Url;
        }

        public ResourceType ResType { get; }

        public string Url { get; }

        /// <summary>
        /// False by default.
        /// </summary>
        public bool UsedManagedRequestHandler { get; }

        /// <summary>
        /// Work only if <see cref="UsedManagedRequestHandler"/> is true.
        /// </summary>
        public string TextContentToReplace { get; set; }



        public string TryGetPostDataString()
        {
            if (_request.PostData == null || _request.PostData.Elements.Count == 0)
                return null;
            var bytes = _request.PostData.Elements[0].Bytes;
            var requestPostDataStr = Encoding.UTF8.GetString(bytes);
            return requestPostDataStr;
        }

        /// <summary>
        /// Will work only if use custom handler.
        /// </summary>
        /// <param name="requestPostDataStr"></param>
        public void TrySetPostDataString(string requestPostDataStr)
        {
            if (_request.PostData == null || _request.PostData.Elements.Count == 0)
                return;
            _request.PostData.Elements[0].Bytes = Encoding.UTF8.GetBytes(requestPostDataStr);
        }
    }

    public class BeforeResourceRequestEventArgs : EventArgs
    {
        readonly IRequest _request;
        readonly HttpClient _httpClient;

        public BeforeResourceRequestEventArgs(IRequest request, HttpClient httpClient)
        {
            _request = request;
            _httpClient = httpClient;
            ResType = request.ResourceType;
            Url = request.Url;
        }

        public ResourceType ResType { get; }

        public string Url { get; }

        /// <summary>
        /// False by default.
        /// </summary>
        public bool UsedManagedRequestHandler => ManagedRequestResponseMessage != null;

        public HttpResponseMessage ManagedRequestResponseMessage { get; private set; }

        public HttpResponseMessage UseManagedRequest(string replacedContent = null)
        {
            var method = HttpClientExt.HttpMethodFromString(_request.Method);
            var url = _request.Url;
            var reqMsg = new HttpRequestMessage(method, url);
            reqMsg.Content = new Content
            ManagedRequestResponseMessage = _httpClient.SendAsync(reqMsg).Result;
            return ManagedRequestResponseMessage;
        }

        public string TryGetPostDataString()
        {
            if (_request.PostData == null || _request.PostData.Elements.Count == 0)
                return null;
            var bytes = _request.PostData.Elements[0].Bytes;
            var requestPostDataStr = Encoding.UTF8.GetString(bytes);
            return requestPostDataStr;
        }

        /// <summary>
        /// Will work only if use custom handler.
        /// </summary>
        /// <param name="requestPostDataStr"></param>
        public void TrySetPostDataString(string requestPostDataStr)
        {
            if (_request.PostData == null || _request.PostData.Elements.Count == 0)
                return;
            _request.PostData.Elements[0].Bytes = Encoding.UTF8.GetBytes(requestPostDataStr);
        }
    }
}