using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IRO.Storage;

namespace ZennoLabBrowser.WinForms.Models
{
    public class AppSavedState
    {
        public IList<SavedPageInfo> OpenPages { get; set; } = new List<SavedPageInfo>();

        public static void Update(Action<AppSavedState> updAct)
        {
            var state = Load();
            updAct?.Invoke(state);
            Save(state);
        }

        public static AppSavedState Load()
        {
            var settings = GlobalObjects.Storage.GetOrDefault<AppSavedState>("saved_state").Result;
            return settings ?? new AppSavedState();
        }

        public static void Save(AppSavedState settings)
        {
            GlobalObjects.Storage.Set("saved_state", settings).Wait();
        }
    }
}