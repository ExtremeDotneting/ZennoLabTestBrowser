namespace ZennoLabBrowser.WinForms.Models
{
    public class RequestReplacerItem
    {
        public string RequestUrlRegex { get; set; }

        public string RequestDataRegex { get; set; }

        public string ReplaceString { get; set; }

        public string MediaType { get; set; } = "text/html";
    }
}