using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CefSharp;

namespace ZennoLabBrowser.WinForms.Services
{
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