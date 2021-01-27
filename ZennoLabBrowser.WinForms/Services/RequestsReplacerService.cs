using System.Text.RegularExpressions;

namespace ZennoLabBrowser.WinForms.Services
{
    public class RequestsReplacerService
    {
        public static RequestsReplacerService Inst { get; } = new RequestsReplacerService();

        public bool IsUrlRedirect(string url, out string redirectUrl)
        {
            var list = GlobalObjects.UserSettings.UrlRedirect;
            foreach (var item in list)
            {
                if (!Regex.IsMatch(url, item.RequestUrlRegex))
                    continue;
                redirectUrl = item.ReplaceUrl;
                return true;
            }
            redirectUrl = null;
            return false;
        }

        public bool IsRequestMustBeReplaced(string url, string requestStr, out string requestReplacedStr)
        {
            var list = GlobalObjects.UserSettings.RequestBodyReplacing;
            foreach (var item in list)
            {
                if (!Regex.IsMatch(url, item.RequestUrlRegex))
                    continue;
                if (requestStr==null || !Regex.IsMatch(requestStr, item.RequestDataRegex))
                    continue;

                requestReplacedStr = Regex.Replace(
                    requestStr,
                    item.RequestDataRegex,
                    item.ReplaceString
                );
                return true;
            }
            requestReplacedStr = null;
            return false;
        }

        public bool IsResponseCanBeReplaced(string url)
        {
            var list = GlobalObjects.UserSettings.ResponseBodyReplacing;
            foreach (var item in list)
            {
                if (Regex.IsMatch(url, item.RequestUrlRegex))
                    return true;
            }
            return true;
        }

        public bool IsResponseMustBeReplaced(string url, string responseStr, out string responseReplacedStr)
        {
            var list = GlobalObjects.UserSettings.ResponseBodyReplacing;
            foreach (var item in list)
            {
                if (!Regex.IsMatch(url, item.RequestUrlRegex))
                    continue;
                if (responseStr == null || !Regex.IsMatch(responseStr, item.ResponseDataRegex))
                    continue;

                responseReplacedStr = Regex.Replace(
                    responseStr,
                    item.ResponseDataRegex,
                    item.ReplaceString
                );
                return true;
            }

            responseReplacedStr = null;
            return false;
        }
    }
}