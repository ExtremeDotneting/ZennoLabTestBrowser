using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using IRO.XWebView.Core;
using ZennoLabBrowser.WinForms.Models;
using ZennoLabBrowser.WinForms.UI;

namespace ZennoLabBrowser.WinForms
{
    public partial class MainForm : DevExpress.XtraBars.TabForm
    {
        static int OpenFormCount = 1;

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

            var state = AppSavedState.Load();
            if (state.OpenPages?.Any() == true)
            {
                var defaultPage = TabFormControl.Pages[0];
                foreach (var item in state.OpenPages)
                {
                    TabFormControl.AddNewPage();
                    var page = TabFormControl.Pages.Last();
                    page.Text = item.Title;
                    var browser = GetBrowserControl(page);
                    browser.XWV.WaitInitialization().ContinueWith(async (t) =>
                    {
                        await browser.XWV.WaitWhileNavigating();
                        await browser.XWV.LoadUrl(item.Url);
                    });
                }
                TabFormControl.ClosePage(defaultPage);
            }

        }

        void InitPage(TabFormPage page)
        {
            page.Text = "Home page";
            var browserCtrl = new CustomBrowserControl();
            page.ContentContainer.Controls.Clear();
            page.ContentContainer.Controls.Add(browserCtrl);

            browserCtrl.PageTitleChanged += (xwv, title) =>
            {
                page.Text = string.IsNullOrWhiteSpace(title) ? "Page" : title;
            };
        }

        void OnSelectPage(TabFormPage page)
        {
            _currentBrowserControl = GetBrowserControl(page);
            _currentBrowserControl.Dock = DockStyle.Fill;
            GlobalObjects.CurrentStatusStripLabel = _currentBrowserControl?.CurrentToolStripStatusLabel;
        }

        void OnOuterFormCreating(object sender, OuterFormCreatingEventArgs e)
        {
            MainForm form = new MainForm();
            form.TabFormControl.Pages.Clear();
            e.Form = form;
            OpenFormCount++;
        }

        private void SettingsButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            var settingsEditorForm = new SettingsEditorForm();
            settingsEditorForm.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var pageUrls = new List<SavedPageInfo>();
            var arr = TabFormControl.Pages.ToArray();
            foreach (var page in arr)
            {
                var browser = GetBrowserControl(page);
                if (browser == null)
                {
                    continue;
                }

                var newItem = new SavedPageInfo()
                {
                    Url = browser.XWV.Url,
                    Title = page.Text
                };
                pageUrls.Add(newItem);
            }
            AppSavedState.Update((state) =>
            {
                state.OpenPages = pageUrls;
            });
        }

        CustomBrowserControl GetBrowserControl(TabFormPage page)
        {
            return page?.ContentContainer?.Controls[0] as CustomBrowserControl;
        }

        private void InfoButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            Process.Start("https://github.com/ExtremeDotneting/ZennoLabTestBrowser");
        }
    }
}
