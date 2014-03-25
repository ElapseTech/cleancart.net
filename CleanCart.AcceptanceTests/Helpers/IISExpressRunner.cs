
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using TechTalk.SpecFlow;

namespace CleanCart.AcceptanceTests.Helpers
{
    [Binding]
    class IISExpressRunner
    {
        public const int Port = 42323;

        private static Process _iisProcess;

        [BeforeFeature]
        public static void EnsureRunning()
        {
            if (_iisProcess == null)
            {
                var thread = new Thread(StartIisExpress) { IsBackground = true };
                thread.Start();
            }
        }

        private static void StartIisExpress()
        {
            var websitePath = FindWebsiteRoot();
            var startInfo = CreateStartInfo(websitePath);
            AddIisAsFileName(startInfo);

            try
            {
                _iisProcess = new Process { StartInfo = startInfo };
                _iisProcess.Start();
                _iisProcess.WaitForExit();
            }
            catch
            {
                EnsureStopped();
            }
        }

        private static string FindWebsiteRoot()
        {
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var websitePath = Path.GetFullPath(Path.Combine(currentDirectory, "..", "..", "..", "CleanCart.Web", "obj", "publish"));
            return websitePath;
        }

        private static ProcessStartInfo CreateStartInfo(string websitePath)
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", websitePath, Port)
            };
            return startInfo;
        }

        private static void AddIisAsFileName(ProcessStartInfo startInfo)
        {
            var programfiles = string.IsNullOrEmpty(startInfo.EnvironmentVariables["programfiles"])
                ? startInfo.EnvironmentVariables["programfiles(x86)"]
                : startInfo.EnvironmentVariables["programfiles"];

            startInfo.FileName = programfiles + "\\IIS Express\\iisexpress.exe";
        }

        [AfterFeature]
        public static void EnsureStopped()
        {
            if (_iisProcess != null && !_iisProcess.HasExited)
            {
                SendStopMessageToIisProcess();
                _iisProcess.Close();
                _iisProcess.Dispose();
            }
        }

        // Black magic : http://stackoverflow.com/questions/4772092/starting-and-stopping-iis-express-programmatically
        private static void SendStopMessageToIisProcess()
        {
            var pid = _iisProcess.Id;
            try
            {
                for (var ptr = NativeMethods.GetTopWindow(IntPtr.Zero); ptr != IntPtr.Zero; ptr = NativeMethods.GetWindow(ptr, 2))
                {
                    uint num;
                    NativeMethods.GetWindowThreadProcessId(ptr, out num);
                    if (pid == num)
                    {
                        var hWnd = new HandleRef(null, ptr);
                        NativeMethods.PostMessage(hWnd, 0x12, IntPtr.Zero, IntPtr.Zero);
                        return;
                    }
                }
            }
            catch (ArgumentException)
            {
            }
        }

        internal class NativeMethods
        {
            // Methods
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr GetTopWindow(IntPtr hWnd);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint lpdwProcessId);
            [DllImport("user32.dll", SetLastError = true)]
            internal static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        }
    }
}
