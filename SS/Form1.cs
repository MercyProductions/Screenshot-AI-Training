using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS
{
    public partial class Form1 : Form
    {
        public static bool EnableSSOnTimer;

        // Import necessary functions from user32.dll
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        // Constants for mouse hook
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        // Delegate for LowLevelMouseProc
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Global variables
        private static IntPtr _hookID = IntPtr.Zero;
        public Form1()
        {
            InitializeComponent();

            _hookID = SetHook(HookCallback);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Folders.CreateFolder("Monitor 1");
            Folders.CreateFolder("Monitor 2");

            StartScreenshotTimer.Start();
        }

        private void StartScreenshotTimer_Tick(object sender, EventArgs e)
        {
            if (EnableSSOnTimer)
            {
                DateTime currentDate = DateTime.Now;
                // Format the date as "Month-Day-Year"
                string dateString = currentDate.ToString("MM-dd-yyyy");

                // Capture screenshots of Monitor 1 and Monitor 2
                using (Bitmap screenshot1 = CaptureScreen(0)) // Monitor 1
                using (Bitmap screenshot2 = CaptureScreen(1)) // Monitor 2
                {
                    // Specify the folder paths for Monitor 1 and Monitor 2
                    string monitor1FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitor 1", dateString);
                    string monitor2FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitor 2", dateString);

                    try
                    {
                        // Create the folders for the current date if they don't exist
                        Folders.CreateFolder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitor 1", dateString));
                        Folders.CreateFolder(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monitor 2", dateString));

                        // Save screenshots as .jpg files in their respective folders
                        string screenshot1FileName = Path.Combine(monitor1FolderPath, $"{currentDate.ToString("HH-mm-ss")}.jpg");
                        string screenshot2FileName = Path.Combine(monitor2FolderPath, $"{currentDate.ToString("HH-mm-ss")}.jpg");
                        screenshot1.Save(screenshot1FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        screenshot2.Save(screenshot2FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions occurred during screenshot capture or saving
                        Console.WriteLine($"Error capturing or saving screenshots: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Bitmap CaptureScreen(int screenIndex)
        {
            // Capture the specified screen
            Screen screen = Screen.AllScreens[screenIndex];
            Rectangle bounds = screen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics gfx = Graphics.FromImage(screenshot))
            {
                gfx.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
            }
            return screenshot;
        }

        private void EnableBtn_Click(object sender, EventArgs e)
        {
            if (EnableBtn.Text == "Start Screenshots On Timer")
            {
                EnableSSOnTimer = true;
                EnableBtn.Text = "Stop Screenshots On Timer";
            }
            else
            {
                if (EnableBtn.Text == "Stop Screenshots On Timer")
                {
                    EnableSSOnTimer = false;
                    EnableBtn.Text = "Start Screenshots On Timer";
                }
            }
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // Check if the left mouse button is clicked
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                // Take a screenshot of Monitor 1 and Monitor 2
                CaptureAndSaveScreenshot("Monitor 1");
                CaptureAndSaveScreenshot("Monitor 2");
            }
            // Check if the right mouse button is clicked
            else if (nCode >= 0 && wParam == (IntPtr)WM_RBUTTONDOWN)
            {
                // Take a screenshot of Monitor 1 and Monitor 2
                CaptureAndSaveScreenshot("Monitor 1");
                CaptureAndSaveScreenshot("Monitor 2");
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static void CaptureAndSaveScreenshot(string monitorName)
        {
            try
            {
                // Capture screenshot of the specified monitor
                Rectangle bounds = Screen.AllScreens[monitorName == "Monitor 1" ? 0 : 1].Bounds;
                using (Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(screenshot))
                    {
                        graphics.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                    }

                    // Get current date
                    DateTime currentDate = DateTime.Now;
                    // Format the date as "Month-Day-Year"
                    string dateString = currentDate.ToString("MM-dd-yyyy");

                    // Specify the path where the screenshot will be saved
                    string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, monitorName, dateString);
                    string fileName = $"{currentDate.ToString("HH-mm-ss")}.jpg";
                    string filePath = Path.Combine(folderPath, fileName);

                    // Create folder if it doesn't exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Save screenshot as .jpg
                    screenshot.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Console.WriteLine($"Screenshot saved to '{filePath}'");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error capturing and saving screenshot: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            LblFPSIssue.Text = "Causing FPS Issues? Adjust Interval :" + trackBar1.Value;
            StartScreenshotTimer.Interval = trackBar1.Value;
        }
    }
}
