using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ZennoLabBrowser.WinForms.Models;
using ZennoLabBrowser.WinForms.Services;

namespace ZennoLabBrowser.WinForms.UI
{
    public partial class SettingsEditorForm : DevExpress.XtraEditors.XtraForm
    {
        public SettingsEditorForm()
        {
            InitializeComponent();
            var settings = UserSettings.Load();
            CurrentPropertyGridControl.SelectedObject = settings;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var settings = (UserSettings) CurrentPropertyGridControl.SelectedObject;
            UserSettings.Save(settings);
            GlobalObjects.UserSettings = settings;
            StatusMessagesService.Write("Some settings will be updated after app restart.");
            Close();
        }

        private void RestoreDefaultButton_Click(object sender, EventArgs e)
        {
            CurrentPropertyGridControl.SelectedObject = new UserSettings();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}