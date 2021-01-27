using System;
using System.IO;
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
        readonly RequestsReplacerService _requestsReplacer = RequestsReplacerService.Inst;

        protected override CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            if (IsTextResourceType(request))
            {
                var url = request.Url;
                var requestStr = TryGetPostDataString(request);
                if (_requestsReplacer.IsRequestMustBeReplaced(url, requestStr, out var requestReplacedStr))
                {
                    TrySetPostDataString(request, requestReplacedStr);
                }
            }
            return base.OnBeforeResourceLoad(chromiumWebBrowser, browser, frame, request, callback);
        }

        protected override IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            if (IsTextResourceType(request))
            {
                var url = request.Url;
                if (_requestsReplacer.IsResponseCanBeReplaced(url))
                {
                    var responseMessage = SendResourceRequest(request).Result;

                    var stream = responseMessage
                        .Content
                        .ReadAsStreamAsync()
                        .Result;
                    Encoding encoding = null;
                    try
                    {
                        encoding = Encoding.GetEncoding(responseMessage.Content.Headers.ContentType.CharSet);
                    }
                    catch
                    {
                        encoding = Encoding.UTF8;
                    }

                    var reader = new StreamReader(stream, encoding);
                    var responseContentStr = reader.ReadToEnd();

                    if (_requestsReplacer.IsResponseMustBeReplaced(url, responseContentStr, out var responseReplacedStr))
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
            if (request.PostData == null || request.PostData.Elements.Count == 0 || request.PostData.Elements[0].Bytes == null)
                return null;

            var bytes = request.PostData.Elements[0].Bytes;
            var requestPostDataStr = Encoding.UTF8.GetString(bytes);
            return requestPostDataStr;
        }

        void TrySetPostDataString(IRequest request, string requestPostDataStr)
        {
            if (request.PostData == null || request.PostData.Elements.Count == 0 || request.PostData.Elements[0].Bytes == null)
                return;
            request.PostData.Elements[0].Bytes = Encoding.UTF8.GetBytes(requestPostDataStr);
        }
    }
}