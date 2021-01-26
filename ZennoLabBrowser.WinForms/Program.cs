using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using IRO.XWebView.CefSharp.Utils;
using IRO.XWebView.CefSharp.WinForms.Utils;
using IRO.XWebView.Core.Utils;
using ZennoLabBrowser.WinForms.Services;

namespace ZennoLabBrowser.WinForms
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var hiddenForm= new Form();
            hiddenForm.Load += delegate
            {
                hiddenForm.Size = new Size(0, 0);
                hiddenForm.ShowInTaskbar = false;
                hiddenForm.FormBorderStyle = FormBorderStyle.None;
                ApplicationStartup();
            };
            //In example we use invisible main form as synchronization context.
            //It's important for ThreadSync that main form must be available during all app lifetime.
            XWebViewThreadSync.Init(new WinFormsThreadSyncInvoker(hiddenForm));
            InitializeCefSharp();
            Application.Run(hiddenForm);
        }

        /// <summary>
        /// Real main work here.
        /// </summary>
        [STAThread]
        static void ApplicationStartup()
        {
            StatusMessagesService.StartAutoClearThread();
            GlobalObjects.MainForm = new MainForm();
            GlobalObjects.MainForm.FormClosed += delegate
            {
                Application.Exit(); 
            };
            GlobalObjects.MainForm.Show();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void InitializeCefSharp()
        {
            var settings = new CefSettings();
            settings.BrowserSubprocessPath = Path.Combine(
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                Environment.Is64BitProcess ? "x64" : "x86",
                "CefSharp.BrowserSubprocess.exe"
            );
            CefHelpers.AddDefaultSettings(settings);
            settings.RemoteDebuggingPort = 9222;
            Cef.Initialize(settings, false, browserProcessHandler: null);
        }

        // Will attempt to load missing assembly from either x86 or x64 subdir
        // Required by CefSharp to load the unmanaged dependencies when running using AnyCPU
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);

                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;
        }
    }
}
