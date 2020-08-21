using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDK.WINFORM
{
    public class WBLoader
    {
        public static async Task<WebBrowser> Create(string html, int timeOut = 10)
        {
            WebBrowser browser = new WebBrowser
            {
                DocumentText = html,
                ScriptErrorsSuppressed = true
            };
            browser.DocumentStream.Flush();
            TaskCompletionSource<bool> PageLoaded = null;
            PageLoaded = new TaskCompletionSource<bool>();
            int TimeElapsed = 0;
            browser.DocumentCompleted += (s, e) =>
            {
                if (browser.ReadyState != WebBrowserReadyState.Complete) return;
                if (PageLoaded.Task.IsCompleted) return; PageLoaded.SetResult(true);
            };
            while (PageLoaded.Task.Status != TaskStatus.RanToCompletion)
            {
                await Task.Delay(10);
                TimeElapsed++;
                if (TimeElapsed >= timeOut * 100) PageLoaded.TrySetResult(true);
            }
            return browser;
        }
    }
}
