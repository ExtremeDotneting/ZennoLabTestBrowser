using System.Net.Http;

namespace ZennoLabBrowser.WinForms.Services
{
    public static class HttpClientExt
    {
        public static HttpMethod HttpMethodFromString(string method)
        {
            var res = HttpMethod.Get;
            switch (method)
            {
                case "GET":
                    res = HttpMethod.Get;
                    break;

                case "POST":
                    res = HttpMethod.Post;
                    break;

                case "PUT":
                    res = HttpMethod.Put;
                    break;

                case "DELETE":
                    res = HttpMethod.Delete;
                    break;

                case "HEAD":
                    res = HttpMethod.Head;
                    break;

                case "OPTIONS":
                    res = HttpMethod.Options;
                    break;

                case "TRACE":
                    res = HttpMethod.Trace;
                    break;
            }

            return res;
        }

    }
}