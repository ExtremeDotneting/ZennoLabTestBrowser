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

        protected override CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            if (IsTextResourceType(request))
            {
                var url = request.Url;
                var requestStr = TryGetPostDataString(request);
                if (IsRequestMustBeReplaced(url, requestStr, out var requestReplacedStr))
                {
                    TrySetPostDataString(request, requestReplacedStr);
                }
            }
            return base.OnBeforeResourceLoad(chromiumWebBrowser, browser, frame, request, callback);
        }

        protected override void OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            base.OnResourceLoadComplete(chromiumWebBrowser, browser, frame, request, response, status, receivedContentLength);
        }

        protected override IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            if (IsTextResourceType(request))
            {
                var url = request.Url;
                if (IsResponseCanBeReplaced(url))
                {
                    var responseMessage = SendResourceRequest(request).Result;
                    var responseContentStr = responseMessage.Content.ReadAsStringAsync().Result;
                  
                    if (IsResponseMustBeReplaced(url, responseContentStr, out var responseReplacedStr))
                    {
                        responseContentStr = responseReplacedStr;
                        var requestHandler = new FromHttpResponseMessage_ResourceHandler(
                            responseMessage,
                            responseContentStr
                        );
                        return requestHandler;
                    }
                
                }
            }
            return base.GetResourceHandler(chromiumWebBrowser, browser, frame, request);
        }

        bool IsTextResourceType(IRequest request)
        {
            var resType = request.ResourceType;
            var isTextResourceType = resType != ResourceType.Image
                                     && resType != ResourceType.Media
                                     && resType != ResourceType.Favicon;
            return isTextResourceType;
        }

        bool IsRequestMustBeReplaced(string url, string requestStr, out string requestReplacedStr)
        {
            requestReplacedStr = null;

            if (requestStr != null && requestStr.Contains("aaaaaaa"))
            {
                requestReplacedStr = requestStr?.Replace("aaaaaaa", "BBBBBBBB");
                return true;
            }
            return false;
        }

        bool IsResponseCanBeReplaced(string url)
        {
            return true;
        }

        bool IsResponseMustBeReplaced(string url, string responseStr, out string responseReplacedStr)
        {
            responseReplacedStr = null;
            if (responseStr != null && responseStr.Contains("aaaaaaa"))
            {
                responseReplacedStr = responseStr?.Replace("aaaaaaa", "BBBBBBBB");
                return true;
            }
            return false;
        }

        async Task<HttpResponseMessage> SendResourceRequest(IRequest request)
        {
            var method = HttpClientExt.HttpMethodFromString(request.Method);
            var url = request.Url;
            var reqMsg = new HttpRequestMessage(method, url);
            var contentStr = TryGetPostDataString(request);
            if (contentStr != null)
            {
                reqMsg.Content = new StringContent(contentStr);
            }
            var resp = await _httpClient.SendAsync(reqMsg);
            return resp;
        }

        string TryGetPostDataString(IRequest request)
        {
            if (request.PostData == null || request.PostData.Elements.Count == 0)
                return null;
            var bytes = request.PostData.Elements[0].Bytes;
            var requestPostDataStr = Encoding.UTF8.GetString(bytes);
            return requestPostDataStr;
        }

        void TrySetPostDataString(IRequest request, string requestPostDataStr)
        {
            if (request.PostData == null || request.PostData.Elements.Count == 0)
                return;
            request.PostData.Elements[0].Bytes = Encoding.UTF8.GetBytes(requestPostDataStr);
        }
    }

}