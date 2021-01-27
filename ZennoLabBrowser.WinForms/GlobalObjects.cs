using System.Windows.Forms;
using IRO.Storage;
using ZennoLabBrowser.WinForms.Models;

namespace ZennoLabBrowser.WinForms
{
    public static class GlobalObjects
    {
        public static MainForm MainForm { get; set; }

        public static ToolStripStatusLabel CurrentStatusStripLabel { get; set; }

        public static IKeyValueStorage Storage { get; set; }

        public static UserSettings UserSettings { get; set; }

    }
}
