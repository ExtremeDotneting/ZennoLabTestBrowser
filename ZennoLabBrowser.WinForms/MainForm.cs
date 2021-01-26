using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ZennoLabBrowser.WinForms.UI;

namespace ZennoLabBrowser.WinForms
{
    public partial class MainForm : DevExpress.XtraBars.TabForm
    {
        CustomBrowserControl _currentBrowserControl;

        public MainForm()
        {
            InitializeComponent();
            var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/main.ico");
            IconOptions.Icon = new Icon(iconPath);

            TabFormControl.PageCreated += (s, e) =>
            {
                InitPage(e.Page);
            };
            TabFormControl.SelectedPageChanged += (s, e) =>
            {
                OnSelectPage(e.Page);
            };
            InitPage(TabFormControl.Pages[0]);
            OnSelectPage(TabFormControl.Pages[0]);

            
        }

        void InitPage(TabFormPage page)
        {
            //var browser = new IRO.XWebView.CefSharp.WinForms.CefSharpXWebViewControl();
            //page.ContentContainer.Controls.Clear();
            //page.ContentContainer.Controls.Add(browser);
            //browser.CurrentBrowser.Load("https://google.com");
            var browserCtrl = new CustomBrowserControl();
            browserCtrl.Dock = DockStyle.Fill;
            page.ContentContainer.Controls.Clear();
            page.ContentContainer.Controls.Add(browserCtrl);
        }

        void OnSelectPage(TabFormPage page)
        {
            _currentBrowserControl = page.ContentContainer.Controls[0] as CustomBrowserControl;
                GlobalObjects.CurrentStatusStripLabel = _currentBrowserControl?.CurrentToolStripStatusLabel;
        }


        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            MainForm form = new MainForm();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }
        static int OpenFormCount = 1;
    }
}
