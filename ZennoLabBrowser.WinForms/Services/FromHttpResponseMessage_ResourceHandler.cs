using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CefSharp;

namespace ZennoLabBrowser.WinForms.Services
{
    public class FromHttpResponseMessage_ResourceHandler : ResourceHandler
    {
        readonly HttpResponseMessage _response;
        readonly string _content;

        public FromHttpResponseMessage_ResourceHandler(HttpResponseMessage response, string content)
        {
            _response = response;
            _content = content;
        }

        public override CefReturnValue ProcessRequestAsync(IRequest request, ICallback callback)
        {
            MimeType = _response.Content.Headers.ContentType.MediaType;
            Stream = GenerateStreamFromString(_content);
            StatusCode = (int)_response.StatusCode;
            foreach (var header in _response.Headers)
            {
                Headers[header.Key] = string.Join(", ", header.Value);
            }
            callback.Continue();
            return CefReturnValue.Continue;
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