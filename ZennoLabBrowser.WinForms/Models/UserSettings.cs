namespace ZennoLabBrowser.WinForms.Models
{
    class UserSettings
    {
        public static UserSettings Inst { get; } = new UserSettings();

        public string HomeUrl { get; set; } = "https://google.com";
    }
}
