using System.Collections.Generic;
using IRO.Storage;

namespace ZennoLabBrowser.WinForms.Models
{
    public class UserSettings
    {
        public string HomeUrl { get; set; } = "https://google.com";

        public string UserAgent { get; set; } =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36";

        public List<RequestReplacerItem> RequestBodyReplacing { get; set; } = new List<RequestReplacerItem>();

        public List<ResponseReplacerItem> ResponseBodyReplacing { get; set; } = new List<ResponseReplacerItem>();

        /// <summary>
        /// Url redirect settings.
        /// </summary>
        public List<UrlRedirectItem> UrlRedirect { get; set; }

        /// <summary>
        /// JS executed when page loaded.
        /// </summary>
        public string JsToExecute { get; set; }

        public static UserSettings Load()
        {
            var settings = GlobalObjects.Storage.GetOrDefault<UserSettings>("user_settings").Result;
            return settings ?? new UserSettings();
        }

        public static void Save(UserSettings settings)
        {
            GlobalObjects.Storage.Set("user_settings", settings).Wait();
        }
    }

}


