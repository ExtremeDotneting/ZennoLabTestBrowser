namespace ZennoLabBrowser.WinForms.Models
{
    public class UrlRedirectItem
    {
        public string RequestUrlRegex { get; set; }

        /// <summary>
        /// If null - go to home page.
        /// </summary>
        public string ReplaceUrl{ get; set; }
    }
}