using EzRealm.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EzRealm
{
    public partial class Oxygen : Form
    {
        public Oxygen()
        {
            InitializeComponent();
            this.ClientSizeChanged += new EventHandler(Form1_Resize);
            cheatMenu.MouseMove += new MouseEventHandler(DragNMove);

            nexusDown.MouseHover += new EventHandler(LeftLit);
            nexusUp.MouseHover += new EventHandler(RightLit);
            nexusDown.MouseLeave += new EventHandler(MouseExit);
            nexusUp.MouseLeave += new EventHandler(MouseExit);

            hpDown.MouseHover += new EventHandler(LeftLit);
            hpUp.MouseHover += new EventHandler(RightLit);
            hpDown.MouseLeave += new EventHandler(MouseExit);
            hpUp.MouseLeave += new EventHandler(MouseExit);

            mpDown.MouseHover += new EventHandler(LeftLit);
            mpUp.MouseHover += new EventHandler(RightLit);
            mpDown.MouseLeave += new EventHandler(MouseExit);
            mpUp.MouseLeave += new EventHandler(MouseExit);

            holdNexHeight = (int)(Screen.PrimaryScreen.Bounds.Height * 0.423148148148148);

            nexusMarker.MouseHover += new EventHandler(nexusMarker_Enter);
            hpMarker.MouseHover += new EventHandler(hpMarker_Enter);
            mpMarker.MouseHover += new EventHandler(mpMarker_Enter);

            overlayBtn.MouseHover += new EventHandler(ovrEnter);
            overlayBtn.MouseLeave += new EventHandler(ovrLeave);

            antiCon.MouseHover += new EventHandler(conEnter);
            antiCon.MouseLeave += new EventHandler(conLeave);
        }

        private void conLeave(object sender, EventArgs e)
        {
            //toolTip1.Show("", antiCon);
           
        }

        private void conEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Using The ↑↓←→ (Arrow Keys) when confused will result in a normal controls for movement [Click to turn on/off] (Ensure nothing is mapped to your arrow-keys)", antiCon);
           
        }

        private void ovrLeave(object sender, EventArgs e)
        {
            if (inOvrMode == false) { overlayBtn.Image = Resources.UP; }
            else { overlayBtn.Image = Resources.DOWN; }

        }

        private void ovrEnter(object sender, EventArgs e)
        {
            if (inOvrMode == false) { overlayBtn.Image = Resources.UP1; }
            else { overlayBtn.Image = Resources.DOWN1; }
        }

        private void MouseExit(object sender, EventArgs e)
        {
            nexusDown.Image = Resources.Left1;
            nexusUp.Image = Resources.Right1;
            hpUp.Image = Resources.Right1;
            hpDown.Image = Resources.Left1;
            mpUp.Image = Resources.Right1;
            mpDown.Image = Resources.Left1;

        }

        private void RightLit(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.Image = Resources.Right;
            }
        }

        private void LeftLit(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                pictureBox.Image = Resources.Left;
            }
        }
        public static Uri Fish = new Uri("file:///" + AppDomain.CurrentDomain.BaseDirectory + "GUI.html");
        private async void Oxygen_Load(object sender, EventArgs e)
        {
            // Design
            cheatMenu.BackColor = Color.FromArgb(26, 26, 28);

            // Custom Keyboard / Hook
            _proc = HookCallback;
            CustomKeyboard customKeyboard = new CustomKeyboard();
            customKeyboard.Register();

            await Task.Delay(3000);

            //Uri Fish = new Uri("file:///" + AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/").Replace(" ", "+") + "GUI.html");
            //webMain.Source = Fish;
            await Task.Delay(700);
           
            ioyke2.Visible = true;
            formClose.Visible = true;
            formMaxi.Visible = true;
            formMini.Visible = true;
            antiCon1.Visible = true;
            antiCon.Visible = true;
            overEzText.Visible = true;
            overlayBtn.Visible = true;
        }



        #region Timers
        public async void SetTimer()
        {
            // if (enableKeysBtn.Checked == true) { try { UnhookWindowsHookEx(_hookID); _hookID = SetHook(_proc); } catch { } }
            // else { try { UnhookWindowsHookEx(_hookID); } catch { } }
            //await Task.Delay(10);
            //SetTimer();
        }
        #endregion

        #region KeyboardHook
        // Low-End Keyboard Hook for Hotkeys ===========================================================================
        //==============================================================================================================
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _hookID = SetHook(_proc);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            try { UnhookWindowsHookEx(_hookID); Environment.Exit(0); } catch { }
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        string currentKey;
        bool showKeyRef;
        bool showColorRef;

        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                // Handle the key press here
                Console.WriteLine("Global key pressed: " + ((Keys)vkCode).ToString());
                currentKey = ((Keys)vkCode).ToString();
                if (showKeyRef == true) { MessageBox.Show(((Keys)vkCode).ToString()); }

                if (((Keys)vkCode).ToString() == "RShiftKey" && showColorRef == true) //Join Tercessuinotlim
                {
                    int x = Cursor.Position.X;
                    int y = Cursor.Position.Y;
                    Color pixelColor = GetPixelColor(x, y);
                    MessageBox.Show(pixelColor.ToString());
                };
                if (currentKey == "23423434") //Join Tercessuinotlim
                {

                    //await Task.Delay(200);
                    var oldPos = Cursor.Position;
                    var newPos = new Point((int)(Screen.PrimaryScreen.Bounds.Width * .86), (int)(Screen.PrimaryScreen.Bounds.Height * .81));
                    //.86 .81
                    Cursor.Position = newPos;
                    Task.Delay(100).Wait(); simulateLeftClick(); simulateLeftClick(); Task.Delay(100).Wait(); simulateLeftClick(); simulateLeftClick(); Task.Delay(100).Wait();
                    Cursor.Position = oldPos;
                };
                if (anti == true)
                {
                    if (((Keys)vkCode).ToString() == "Up") //Join Tercessuinotlim
                    {
                        SimulateKeyPressD();
                    };
                    if (((Keys)vkCode).ToString() == "Down") //Join Tercessuinotlim
                    {
                        SimulateKeyPressA();
                    };
                    if (((Keys)vkCode).ToString() == "Left") //Join Tercessuinotlim
                    {
                        SimulateKeyPressS();
                    };
                    if (((Keys)vkCode).ToString() == "Right") //Join Tercessuinotlim
                    {
                        SimulateKeyPressW();
                    };
                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(
            int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(
            IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        // Low-End Keyboard Hook for Hotkeys ===========================================================================
        //==============================================================================================================
        #endregion

        #region SimulateMouseClick
        // Mouse Click Controller
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_MOVE = 0x0001;

        public void simulateLeftClick()
        {
#pragma warning disable // Rethrow to preserve stack details
            mouse_event(MOUSEEVENTF_LEFTDOWN, 1, 1, 1, 1);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
#pragma warning restore // Rethrow to preserve stack details
        }
        // Mouse Click Controller
        #endregion

        #region PixelColorControllers
        public Color GetPixelColor(int x, int y)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(x, y, 0, 0, new Size(1, 1));
                }

                return bitmap.GetPixel(0, 0);
            }
        }

        class PixelColorMonitor : IDisposable
        {
            private int x, y;
            public Color currentColor;
            private Task colorCheckTask;
            private bool isMonitoring;

            // define a custom event for when the pixel color changes
            public event EventHandler PixelColorChanged;

            public PixelColorMonitor(int x, int y)
            {
                this.x = x;
                this.y = y;
                isMonitoring = true;
                colorCheckTask = Task.Run(() => MonitorPixelColor());
            }

            // continuously monitor the color of the pixel at the specified location
            private async void MonitorPixelColor()
            {
                while (isMonitoring)
                {
                    // get the color of the pixel
                    Color pixelColor = GetPixelColor(x, y);

                    // check if the pixel color has changed
                    if (pixelColor != currentColor)
                    {
                        currentColor = pixelColor;
                        // raise the PixelColorChanged event
                        PixelColorChanged?.Invoke(this, EventArgs.Empty);
                    }

                    // sleep for a short amount of time before checking again
                    await Task.Delay(5);
                }
            }

            // get the color of the pixel at the specified location
            private Color GetPixelColor(int x, int y)
            {
                Bitmap bmp = new Bitmap(1, 1);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(new Point(x, y), Point.Empty, new Size(1, 1));
                }
                return bmp.GetPixel(0, 0);
            }

            // stop monitoring and dispose of the object
            public void Dispose()
            {
                isMonitoring = false;
                colorCheckTask.Wait();
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        private async void Monitor_PixelColorChanged(object sender, EventArgs e)
        {
            string windowTitle = "RotMGExalt";
            IntPtr hwnd = FindWindow(null, windowTitle);

            if (hwnd == IntPtr.Zero)
            {
                // Window not found
                MessageBox.Show("Game Window not Found Please Restart");
            }
            else
            {
                // Do something with the window handle
                // handle the pixel color change event
                //MessageBox.Show(AutoNex.Value.ToString());
                try
                {
                    if (nTag == true)
                    {
                        int x = (int)holdNexScroll;
                        //int x = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + AutoNex.Value.ToString()));
                        int y = (int)holdNexHeight;
                        Color pixelColor = GetPixelColor(x, y);
                        if (pixelColor.ToString() == Color.FromArgb(255, 0, 0, 0).ToString())
                        {

                            //MessageBox.Show(pixelColor.ToString() + " " + Color.FromArgb(255, 0, 0, 0).ToString());
                        }
                        if (pixelColor.ToString() == Color.FromArgb(255, 255, 255, 255).ToString() || pixelColor.ToString() == Color.FromArgb(255, 84, 84, 84).ToString())
                        {
                            // Get the handle of the target window, for example:
                            IntPtr handle = hwnd;

                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressR();
                        }
                        else if (pixelColor.ToString() == Color.FromArgb(255, 84, 84, 84).ToString())
                        {
                            IntPtr handle = hwnd;

                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressR();

                        }
                        else if (pixelColor.ToString() == Color.FromArgb(255, 255, 255, 255).ToString())
                        {
                            IntPtr handle = hwnd;

                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressR();

                        }
                    }

                }
                catch { }

            }


            //MessageBox.Show(pixelColor.ToString());
        }

        private async void Monitor_PixelColorChangedh(object sender, EventArgs e)
        {
            string windowTitle = "RotMGExalt";
            IntPtr hwnd = FindWindow(null, windowTitle);

            if (hwnd == IntPtr.Zero)
            {
                // Window not found
                MessageBox.Show("Game Window not Found Please Restart");
            }
            else
            {
                // Do something with the window handle
                // handle the pixel color change event
                //MessageBox.Show(AutoNex.Value.ToString());
                try
                {
                    if (mTag == true)
                    {
                        int x = (int)holdManaScroll;
                        //int x = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + AutoNex.Value.ToString()));
                        int y = ((int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148));

                        Color pixelColor = GetPixelColor(x, y);
                        if (pixelColor.ToString() == Color.FromArgb(255, 0, 0, 0).ToString())
                        {

                            //MessageBox.Show(pixelColor.ToString() + " " + Color.FromArgb(255, 0, 0, 0).ToString());
                        }
                        if (pixelColor.ToString() == Color.FromArgb(255, 255, 255, 255).ToString() || pixelColor.ToString() == Color.FromArgb(255, 84, 84, 84).ToString())
                        {
                            // Get the handle of the target window, for example:
                            IntPtr handle = hwnd;
                            //MessageBox.Show("Attempting Send Key");
                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressV();
                        }
                        else if (pixelColor.ToString() == Color.FromArgb(255, 84, 84, 84).ToString())
                        {
                            IntPtr handle = hwnd;
                            //MessageBox.Show("Attempting Send Key");
                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressV();

                        }
                        else if (pixelColor.ToString() == Color.FromArgb(255, 255, 255, 255).ToString())
                        {
                            IntPtr handle = hwnd;
                           // MessageBox.Show("Attempting Send Key");
                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressV();

                        }
                    }
                }
                catch { }

            }


            //MessageBox.Show(pixelColor.ToString());
        }

        private async void Monitor_PixelColorChangedm(object sender, EventArgs e)
        {
            string windowTitle = "RotMGExalt";
            IntPtr hwnd = FindWindow(null, windowTitle);

            if (hwnd == IntPtr.Zero)
            {
                // Window not found
                MessageBox.Show("Game Window not Found Please Restart");
            }
            else
            {
                // Do something with the window handle
                // handle the pixel color change event
                //MessageBox.Show(AutoNex.Value.ToString());
                try
                {
                    //Health Pot
                    if (hTag == true)
                    {
                        int x = (int)holdPotScroll;
                        //int x = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + AutoNex.Value.ToString()));
                        int y = holdNexHeight;
                        Color pixelColor = GetPixelColor(x, y);
                        if (pixelColor.ToString() == Color.FromArgb(255, 0, 0, 0).ToString())
                        {

                            //MessageBox.Show(pixelColor.ToString() + " " + Color.FromArgb(255, 0, 0, 0).ToString());
                        }
                        if (pixelColor.ToString() == Color.FromArgb(255, 255, 255, 255).ToString() || pixelColor.ToString() == Color.FromArgb(255, 84, 84, 84).ToString())
                        {
                            // Get the handle of the target window, for example:
                            IntPtr handle = hwnd;

                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressF();
                        }
                        else if (pixelColor.ToString() == Color.FromArgb(255, 84, 84, 84).ToString())
                        {
                            IntPtr handle = hwnd;

                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressF();

                        }
                        else if (pixelColor.ToString() == Color.FromArgb(255, 255, 255, 255).ToString())
                        {
                            IntPtr handle = hwnd;

                            // Set the focus to the target window
                            SetForegroundWindow(handle);
                            SimulateKeyPressF();

                        }
                    }
                }
                catch { }

            }


            //MessageBox.Show(pixelColor.ToString());
        }

        #endregion

        #region MoveBrowser
        // Move Top Border Code
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public static void DragNMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Oxygen.ActiveForm.FindForm().Handle, 0xA1, HT_CAPTION, 0);
                Oxygen.ActiveForm.Update();

            }
        }
        // Move Top Border Code
        #endregion

        #region Resize Settings
        private void Form1_Resize(object sender, EventArgs e)
        {

            int radius = 26;
            GraphicsPath path = new GraphicsPath();
            int arcPoints = 2; // Number of points to define the arc

            // Enable anti-aliasing
            Graphics graphics = CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Top left corner
            path.AddArc(0, 0, radius * 2, radius * 2, 180, arcPoints);
            path.AddLine(radius, 0, this.Width - radius, 0);

            // Top right corner
            path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, arcPoints);
            path.AddLine(this.Width, radius, this.Width, this.Height - radius);

            // Bottom right corner
            path.AddArc(this.Width - radius * 2, this.Height - radius * 2, radius * 2, radius * 2, 0, arcPoints);
            path.AddLine(this.Width - radius, this.Height, radius, this.Height);

            // Bottom left corner
            path.AddArc(0, this.Height - radius * 2, radius * 2, radius * 2, 90, arcPoints);
            path.AddLine(0, this.Height - radius, 0, radius);

            this.Region = new Region(path);

            // Dispose the Graphics object
            graphics.Dispose();

        }

        private bool isResizing = false;

        protected override void OnResizeBegin(EventArgs e)
        {
            base.OnResizeBegin(e);
            isResizing = true;
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            isResizing = false;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!isResizing)
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    int radius = 26;

                    int arcPoints = 2; // Number of points to define the arc

                    // Enable anti-aliasing
                    Graphics graphics = CreateGraphics();
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Top left corner
                    path.AddArc(0, 0, radius * 2, radius * 2, 180, arcPoints);
                    path.AddLine(radius, 0, this.Width - radius, 0);

                    // Top right corner
                    path.AddArc(this.Width - radius * 2, 0, radius * 2, radius * 2, 270, arcPoints);
                    path.AddLine(this.Width, radius, this.Width, this.Height - radius);

                    // Bottom right corner
                    path.AddArc(this.Width - radius * 2, this.Height - radius * 2, radius * 2, radius * 2, 0, arcPoints);
                    path.AddLine(this.Width - radius, this.Height, radius, this.Height);

                    // Bottom left corner
                    path.AddArc(0, this.Height - radius * 2, radius * 2, radius * 2, 90, arcPoints);
                    path.AddLine(0, this.Height - radius, 0, radius);
                    // using (Pen pen = new Pen(Color.FromArgb(255, 232, 234, 235), 4))
                    // {
                    //  e.Graphics.DrawPath(pen, path);
                    // e.Graphics.DrawPath(pen, path);
                    //  e.Graphics.DrawPath(pen, path);
                    //}
                    this.Region = new Region(path);

                    // Dispose the Graphics object
                    graphics.Dispose();
                    // Outline
                    //using (Pen pen = new Pen(Color.FromArgb(255, 1, 1, 1), 1))
                    //{
                    //  e.Graphics.DrawPath(pen, path);
                    //  }

                    Region = new Region(path);
                }
            }
        }
        #endregion 

        #region AISizeMe

        private const int GripSize = 16;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        const int WM_NCLBUTTONDOWN = 0x00A1;

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_NCHITTEST && (int)message.Result == HTCLIENT)
            {
                Point screenPoint = new Point(message.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                if (clientPoint.X <= GripSize)
                {
                    if (clientPoint.Y <= GripSize)
                        message.Result = (IntPtr)HTTOPLEFT;
                    else if (clientPoint.Y >= this.ClientSize.Height - GripSize)
                        message.Result = (IntPtr)HTBOTTOMLEFT;
                    else
                        message.Result = (IntPtr)HTLEFT;
                }
                else if (clientPoint.X >= this.ClientSize.Width - GripSize)
                {
                    if (clientPoint.Y <= GripSize)
                        message.Result = (IntPtr)HTTOPRIGHT;
                    else if (clientPoint.Y >= this.ClientSize.Height - GripSize)
                        message.Result = (IntPtr)HTBOTTOMRIGHT;
                    else
                        message.Result = (IntPtr)HTRIGHT;
                }
                else if (clientPoint.Y <= GripSize)
                {
                    message.Result = (IntPtr)HTTOP;
                }
                else if (clientPoint.Y >= this.ClientSize.Height - GripSize)
                {
                    message.Result = (IntPtr)HTBOTTOM;
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                Point screenPoint = this.PointToScreen(e.Location);

                if (screenPoint.X <= this.Location.X + GripSize)
                {
                    if (screenPoint.Y <= this.Location.Y + GripSize)
                        this.Cursor = Cursors.SizeNWSE;
                    else if (screenPoint.Y >= this.Location.Y + this.Height - GripSize)
                        this.Cursor = Cursors.SizeNESW;
                    else
                        this.Cursor = Cursors.SizeWE;
                }
                else if (screenPoint.X >= this.Location.X + this.Width - GripSize)
                {
                    if (screenPoint.Y <= this.Location.Y + GripSize)
                        this.Cursor = Cursors.SizeNESW;
                    else if (screenPoint.Y >= this.Location.Y + this.Height - GripSize)
                        this.Cursor = Cursors.SizeNWSE;
                    else
                        this.Cursor = Cursors.SizeWE;
                }
                else if (screenPoint.Y <= this.Location.Y + GripSize)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (screenPoint.Y >= this.Location.Y + this.Height - GripSize)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        // private const int WM_NCLBUTTONDOWN = 0xA1;
        // private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        // [DllImport("user32.dll")]
        // private static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point screenPoint = this.PointToScreen(e.Location);

                if (screenPoint.X <= this.Location.X + GripSize)
                {
                    if (screenPoint.Y <= this.Location.Y + GripSize)
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTTOPLEFT, IntPtr.Zero);
                    else if (screenPoint.Y >= this.Location.Y + this.Height - GripSize)
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTBOTTOMLEFT, IntPtr.Zero);
                    else
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTLEFT, IntPtr.Zero);
                }
                else if (screenPoint.X >= this.Location.X + this.Width - GripSize)
                {
                    if (screenPoint.Y <= this.Location.Y + GripSize)
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTTOPRIGHT, IntPtr.Zero);
                    else if (screenPoint.Y >= this.Location.Y + this.Height - GripSize)
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTBOTTOMRIGHT, IntPtr.Zero);
                    else
                        SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTRIGHT, IntPtr.Zero);
                }
                else if (screenPoint.Y <= this.Location.Y + GripSize)
                {
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTTOP, IntPtr.Zero);
                }
                else if (screenPoint.Y >= this.Location.Y + this.Height - GripSize)
                {
                    SendMessage(this.Handle, WM_NCLBUTTONDOWN, (IntPtr)HTBOTTOM, IntPtr.Zero);
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region CustomKeyboard
        public class CustomKeyboard : NativeWindow
        {
            [DllImport("user32.dll")]
            private static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

            [DllImport("user32.dll")]
            private static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

            private const ushort HID_USAGE_PAGE_GENERIC = 0x01;
            private const ushort HID_USAGE_GENERIC_KEYBOARD = 0x06;
            private const uint RIDEV_INPUTSINK = 0x00000100;
            private const int WM_INPUT = 0x00FF;

            public CustomKeyboard()
            {
                CreateHandle(new CreateParams());
                //Application.Run();
            }

            public void Register()
            {
                string windowTitle = "RotMGExalt";
                IntPtr hwnd = FindWindow(null, windowTitle);
                RAWINPUTDEVICE[] devices = new RAWINPUTDEVICE[1];
                devices[0].usUsagePage = HID_USAGE_PAGE_GENERIC;
                devices[0].usUsage = HID_USAGE_GENERIC_KEYBOARD;
                devices[0].dwFlags = RIDEV_INPUTSINK;
                devices[0].hwndTarget = hwnd;

                if (!RegisterRawInputDevices(devices, (uint)devices.Length, (uint)Marshal.SizeOf(typeof(RAWINPUTDEVICE))))
                {
                    // Handle registration error here
                }
            }

            protected override void WndProc(ref Message message)
            {
                if (message.Msg == WM_INPUT)
                {
                    uint size = 0;
                    GetRawInputData(message.LParam, 0x10000003, IntPtr.Zero, ref size, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));
                    byte[] buffer = new byte[size];
                    if (GetRawInputData(message.LParam, 0x10000003, Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), ref size, (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == size)
                    {
                        RAWINPUTHEADER header = (RAWINPUTHEADER)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), typeof(RAWINPUTHEADER));
                        if (header.dwType == 1)
                        {
                            RAWINPUT keyboardInput = (RAWINPUT)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), typeof(RAWINPUT));
                            // Handle keyboard input here
                        }
                    }
                }

                base.WndProc(ref message);
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct RAWINPUTDEVICE
            {
                public ushort usUsagePage;
                public ushort usUsage;
                public uint dwFlags;
                public IntPtr hwndTarget;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct RAWINPUTHEADER
            {
                public uint dwType;
                public uint dwSize;
                public IntPtr hDevice;
                public IntPtr wParam;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct RAWINPUT
            {
                public RAWINPUTHEADER header;
                public RAWKEYBOARD keyboard;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct RAWKEYBOARD
            {
                public ushort MakeCode;
                public ushort Flags;
                public ushort Reserved;
                public ushort VKey;
                public uint Message;
                public uint ExtraInformation;
            }
        }
        #endregion

        #region SimulateKeyPresses
        private const uint WM_KEYUP = 0x0101;
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private const byte VK_RMENU = 0x52; // Right Alt key code
        private const byte VK_R = 0x52; // "R" key code
        public void SimulateKeyPressR()
        {

            // Send key down event
            keybd_event(VK_R, 0, 0, 0);

            // Send key up event
            keybd_event(VK_R, 0, 0x0002, 0);

        }
        public void SimulateKeyPressF()
        {

            // Send key down event
            keybd_event(0x46, 0, 0, 0);

            // Send key up event
            keybd_event(0x46, 0, 0x0002, 0);

        }
        public void SimulateKeyPressV()
        {

            // Send key down event
            keybd_event(0x56, 0, 0, 0);

            // Send key up event
            keybd_event(0x56, 0, 0x0002, 0);

        }

        public bool wOK;
        public bool aOK;
        public bool sOK;
        public bool dOK;
        public async void SimulateKeyPressW()
        {
            if (wOK == false)
            {
                wOK = true;
                // Send key down event
                keybd_event(0x57, 0, 0, 0);
                await Task.Delay(340);
                // Send key up event
                keybd_event(0x57, 0, 0x0002, 0);
                wOK = false;
            }


        }
        public async void SimulateKeyPressA()
        {
            if (aOK == false)
            {
                aOK = true;

                // Send key down event
                keybd_event(0x41, 0, 0, 0);
                await Task.Delay(340);
                // Send key up event
                keybd_event(0x41, 0, 0x0002, 0);
                aOK = false;
            }

        }
        public async void SimulateKeyPressS()
        {
            if (sOK == false)
            {
                sOK = true;

                // Send key down event
                keybd_event(0x53, 0, 0, 0);
                await Task.Delay(340);
                // Send key up event
                keybd_event(0x53, 0, 0x0002, 0);
                sOK = false;
            }

        }
        public async void SimulateKeyPressD()
        {
            if (dOK == false)
            {
                dOK = true;
                // Send key down event
                keybd_event(0x44, 0, 0, 0);
                await Task.Delay(340);
                // Send key up event
                keybd_event(0x44, 0, 0x0002, 0);
                dOK = false;
            }

        }
        #endregion

        public bool nTag;
        public bool hTag;
        public bool mTag = false;

        public static double holdNexScroll;
        public static int holdNexHeight;

        PixelColorMonitor monitor;
        PixelColorMonitor hmonitor;
        PixelColorMonitor mmonitor;
        private void nexusToggle_Click(object sender, EventArgs e)
        {
            if (nTag != true)
            {
                nexusToggle.Image = Resources.ON; nTag = true;
                try { monitor.Dispose(); } catch { }
                //holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "9788"));
                //MessageBox.Show(holdNexScroll.ToString());
                monitor = new PixelColorMonitor((int)holdNexScroll, holdNexHeight);
                monitor.PixelColorChanged += Monitor_PixelColorChanged;

            }
            else { nexusToggle.Image = Resources.OFF; nTag = false; }
            if (nexusToggle.Image == Resources.ON)
            {

            }
        }

        double holdPotScroll;
        double holdManaScroll;

        private void hpToggle_Click(object sender, EventArgs e)
        {
            if (hTag != true)
            {
                hpToggle.Image = Resources.ON; hTag = true;
                try { hmonitor.Dispose(); } catch { }
                // holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "9788"));
                //MessageBox.Show(holdNexScroll.ToString());
                hmonitor = new PixelColorMonitor((int)holdPotScroll, holdNexHeight);
                hmonitor.PixelColorChanged += Monitor_PixelColorChangedm;
            }
            else { hpToggle.Image = Resources.OFF; hTag = false; }
        }

        private void mpToggle_Click(object sender, EventArgs e)
        {
            if (mTag != true)
            {
                mpToggle.Image = Resources.ON; mTag = true; mTag = true;
                try { mmonitor.Dispose(); } catch { }
                // holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "9788"));
                //MessageBox.Show(holdNexScroll.ToString());
                mmonitor = new PixelColorMonitor((int)holdManaScroll, (int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148));
                mmonitor.PixelColorChanged += Monitor_PixelColorChangedh;
                //debugNexHPloc.Location = new Point((int)holdManaScroll, (int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148)); //0.03796333
                //MessageBox.Show("activated mp toggle test");
            }
            else { mpToggle.Image = Resources.OFF; mTag = false; }
        }

        public static int nCount = 1;
        public static int hCount = 1;
        public static int mCount = 1;

        private void nexusUp_Click(object sender, EventArgs e)
        {
            if (nCount == 10) { nexusMarker.Image = Resources.Nexus_100; }
            else if (nCount == 9) { nexusMarker.Image = Resources.Nexus_100; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "985")); }
            else if (nCount == 8) { nexusMarker.Image = Resources.Nexus_90; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "970")); }
            else if (nCount == 7) { nexusMarker.Image = Resources.Nexus_80; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "955")); }
            else if (nCount == 6) { nexusMarker.Image = Resources.Nexus_70; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "940")); }
            else if (nCount == 5) { nexusMarker.Image = Resources.Nexus_60; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "925")); }
            else if (nCount == 4) { nexusMarker.Image = Resources.Nexus_50; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "910")); }
            else if (nCount == 3) { nexusMarker.Image = Resources.Nexus_40; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "895")); }
            else if (nCount == 2) { nexusMarker.Image = Resources.Nexus_30; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "880")); }
            else if (nCount == 1) { nexusMarker.Image = Resources.Nexus_20; nCount++; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "865")); }
            if (nTag == true)
            {
                try { monitor.Dispose(); } catch { }

                //MessageBox.Show(holdNexScroll.ToString());
                monitor = new PixelColorMonitor((int)holdNexScroll, holdNexHeight);//holdNexHeight);
                debugNexHPloc.Location = new Point((int)holdNexScroll - 19, holdNexHeight);
                monitor.PixelColorChanged += Monitor_PixelColorChanged;

            }
            if (nTag == false) { BringToFront(); MessageBox.Show("Auto Nexus MUST be on before adjusting the settings or the effect will not take properly."); }
        }

        private void nexusDown_Click(object sender, EventArgs e)
        {
            if (nCount == 10) { nexusMarker.Image = Resources.Nexus_100; }
            else if (nCount == 9) { nexusMarker.Image = Resources.Nexus_90; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "985")); }
            else if (nCount == 8) { nexusMarker.Image = Resources.Nexus_80; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "970")); }
            else if (nCount == 7) { nexusMarker.Image = Resources.Nexus_70; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "955")); }
            else if (nCount == 6) { nexusMarker.Image = Resources.Nexus_60; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "940")); }
            else if (nCount == 5) { nexusMarker.Image = Resources.Nexus_50; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "925")); }
            else if (nCount == 4) { nexusMarker.Image = Resources.Nexus_40; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "910")); }
            else if (nCount == 3) { nexusMarker.Image = Resources.Nexus_30; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "895")); }
            else if (nCount == 2) { nexusMarker.Image = Resources.Nexus_20; nCount--; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "880")); }
            else if (nCount == 1) { nexusMarker.Image = Resources.Nexus_10; debugNcount.Text = nCount.ToString(); holdNexScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "865")); }
            if (nTag == true)
            {
                try { monitor.Dispose(); } catch { }

                //MessageBox.Show(holdNexScroll.ToString());
                monitor = new PixelColorMonitor((int)holdNexScroll, holdNexHeight);//holdNexHeight);
                debugNexHPloc.Location = new Point((int)holdNexScroll - 19, holdNexHeight);
                monitor.PixelColorChanged += Monitor_PixelColorChanged;

            }
            if (nTag == false) { BringToFront(); MessageBox.Show("Auto Nexus MUST be on before adjusting the settings or the effect will not take properly."); }

        }

        private void hpDown_Click(object sender, EventArgs e)
        {
            if (hCount == 10) { hpMarker.Image = Resources.HealthPot_100; }
            else if (hCount == 9) { hpMarker.Image = Resources.HealthPot_90; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "985")); }
            else if (hCount == 8) { hpMarker.Image = Resources.HealthPot_80; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "970")); }
            else if (hCount == 7) { hpMarker.Image = Resources.HealthPot_70; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "955")); }
            else if (hCount == 6) { hpMarker.Image = Resources.HealthPot_60; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "940")); }
            else if (hCount == 5) { hpMarker.Image = Resources.HealthPot_50; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "925")); }
            else if (hCount == 4) { hpMarker.Image = Resources.HealthPot_40; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "910")); }
            else if (hCount == 3) { hpMarker.Image = Resources.HealthPot_30; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "895")); }
            else if (hCount == 2) { hpMarker.Image = Resources.HealthPot_20; hCount--; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "880")); }
            else if (hCount == 1) { hpMarker.Image = Resources.HealthPot_10; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "865")); }
            if (hTag == true)
            {
                try { hmonitor.Dispose(); } catch { }

                //MessageBox.Show(holdNexScroll.ToString());
                hmonitor = new PixelColorMonitor((int)holdPotScroll, holdNexHeight);//holdNexHeight);
                debugNexHPloc.Location = new Point((int)holdPotScroll - 19, holdNexHeight);
                hmonitor.PixelColorChanged += Monitor_PixelColorChangedm;

            }
            if (hTag == false) { BringToFront(); MessageBox.Show("Auto Health Pot MUST be on before adjusting the settings or the effect will not take properly."); }
        }

        private void hpUp_Click(object sender, EventArgs e)
        {
            if (hCount == 10) { hpMarker.Image = Resources.HealthPot_100; }
            else if (hCount == 9) { hpMarker.Image = Resources.HealthPot_100; holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "985")); }
            else if (hCount == 8) { hpMarker.Image = Resources.HealthPot_90; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "970")); }
            else if (hCount == 7) { hpMarker.Image = Resources.HealthPot_80; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "955")); }
            else if (hCount == 6) { hpMarker.Image = Resources.HealthPot_70; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "940")); }
            else if (hCount == 5) { hpMarker.Image = Resources.HealthPot_60; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "925")); }
            else if (hCount == 4) { hpMarker.Image = Resources.HealthPot_50; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "910")); }
            else if (hCount == 3) { hpMarker.Image = Resources.HealthPot_40; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "895")); }
            else if (hCount == 2) { hpMarker.Image = Resources.HealthPot_30; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "880")); }
            else if (hCount == 1) { hpMarker.Image = Resources.HealthPot_20; hCount++; debugHcount.Text = hCount.ToString(); holdPotScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "865")); }
            if (hTag == true)
            {
                try { hmonitor.Dispose(); } catch { }

                //MessageBox.Show(holdNexScroll.ToString());
                hmonitor = new PixelColorMonitor((int)holdPotScroll, holdNexHeight);//holdNexHeight);
                debugNexHPloc.Location = new Point((int)holdPotScroll - 19, holdNexHeight);
                hmonitor.PixelColorChanged += Monitor_PixelColorChangedm;

            }
            if (hTag == false) { BringToFront(); MessageBox.Show("Auto Health Pot MUST be on before adjusting the settings or the effect will not take properly."); }
        }

        private void mpDown_Click(object sender, EventArgs e)
        {
            if (mCount == 10) { mpMarker.Image = Resources.MagicPot_100; }
            else if (mCount == 9) { mpMarker.Image = Resources.MagicPot_80; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "985")); }
            else if (mCount == 8) { mpMarker.Image = Resources.MagicPot_70; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "970")); }
            else if (mCount == 7) { mpMarker.Image = Resources.MagicPot_60; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "955")); }
            else if (mCount == 6) { mpMarker.Image = Resources.MagicPot_50; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "940")); }
            else if (mCount == 5) { mpMarker.Image = Resources.MagicPot_40; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "925")); }
            else if (mCount == 4) { mpMarker.Image = Resources.MagicPot_30; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "910")); }
            else if (mCount == 3) { mpMarker.Image = Resources.MagicPot_20; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "895")); }
            else if (mCount == 2) { mpMarker.Image = Resources.MagicPot_10; mCount--; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "875")); }
            else if (mCount == 1) { mpMarker.Image = Resources.MagicPot_10; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "865")); }
            if (mTag == true)
            {
                try { mmonitor.Dispose(); } catch { }

                //MessageBox.Show(holdNexScroll.ToString());
                mmonitor = new PixelColorMonitor((int)holdManaScroll, (int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148));//holdNexHeight);
                debugNexHPloc.Location = new Point((int)holdManaScroll - 19, (int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148));
                mmonitor.PixelColorChanged += Monitor_PixelColorChangedh;

            }
            if (mTag == false) { BringToFront(); MessageBox.Show("Auto Magic Pot MUST be on before adjusting the settings or the effect will not take properly."); }
        }

        private void mpUp_Click(object sender, EventArgs e)
        {
            if (mCount == 10) { mpMarker.Image = Resources.MagicPot_100; }
            else if (mCount == 9) { mpMarker.Image = Resources.MagicPot_100; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "985")); }
            else if (mCount == 8) { mpMarker.Image = Resources.MagicPot_90; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "970")); }
            else if (mCount == 7) { mpMarker.Image = Resources.MagicPot_80; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "955")); }
            else if (mCount == 6) { mpMarker.Image = Resources.MagicPot_70; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "940")); }
            else if (mCount == 5) { mpMarker.Image = Resources.MagicPot_60; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "925")); }
            else if (mCount == 4) { mpMarker.Image = Resources.MagicPot_50; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "910")); }
            else if (mCount == 3) { mpMarker.Image = Resources.MagicPot_40; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "895")); }
            else if (mCount == 2) { mpMarker.Image = Resources.MagicPot_30; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "880")); }
            else if (mCount == 1) { mpMarker.Image = Resources.MagicPot_20; mCount++; debugMcount.Text = mCount.ToString(); holdManaScroll = (int)(Screen.PrimaryScreen.Bounds.Width * Double.Parse("0." + "865")); }
            if (mTag == true)
            {
                try { mmonitor.Dispose(); } catch { }

                //MessageBox.Show(holdNexScroll.ToString());
                mmonitor = new PixelColorMonitor((int)holdManaScroll, (int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148));//holdNexHeight);
                debugNexHPloc.Location = new Point((int)holdManaScroll - 19, (int)(Screen.PrimaryScreen.Bounds.Height * 0.461148148148148));
                mmonitor.PixelColorChanged += Monitor_PixelColorChangedh;

            }
            if (mTag == false) { BringToFront(); MessageBox.Show("Auto Magic Pot MUST be on before adjusting the settings or the effect will not take properly."); }
        }

        private void debugNcount_Click(object sender, EventArgs e)
        {

        }

        private void nexusMarker_Click(object sender, EventArgs e)
        { }

        private void nexusMarker_Enter(object sender, EventArgs e)
        {

            toolTip1.Show("Auto Nexus - Use this feature for automatic transport to the Nexus when you are in danger. \r\n The Slider is based on your HP. (Does not prevent one shots :/ in most cases)", nexusMarker);
        }

        private void hpMarker_Enter(object sender, EventArgs e)
        {
            toolTip1.Show("Auto Health Pot - Use this feature for automatic healthpot use when your health gets low. \r\n The Slider is based on your HP. (can heal you in a pinch or in battle)", hpMarker);
        }

        private void mpMarker_Enter(object sender, EventArgs e)
        {
            toolTip1.Show("Auto Magic Pot - Use this feature for automatic magicpot use when your magicka gets low. \r\n The Slider is based on your MP. (can help you keep up your magicka in battle)", mpMarker);
        }

        Form Overlay;
        bool inOvrMode;
        private void overlayBtn_Click(object sender, EventArgs e)
        {
            if (inOvrMode == false)
            {
                inOvrMode = true;
                overlayBtn.Image = Resources.DOWN1;

                Overlay = new Form();

                Overlay.FormBorderStyle = FormBorderStyle.None;
                Overlay.Width = Screen.PrimaryScreen.Bounds.Width;
                Overlay.Height = (int)(Screen.PrimaryScreen.Bounds.Height * 0.02);
                Overlay.BackColor = Color.FromArgb(1, 1, 1);
                Overlay.Show();
                Overlay.Location = new Point(0, 0);
                Overlay.Opacity = 0.89;
                Overlay.TopMost = true;
                var exText = new Label();
                Overlay.Controls.Add(exText);
                exText.Text = "EzOverlay";
                exText.ForeColor = Color.White;
                exText.Font = new Font("Impact", 15);
                exText.AutoSize = true;
                Overlay.FormClosing += Overlay_FormClosing;
            }
            else if (inOvrMode == true)
            {
                Overlay.Close();
            }

        }

        private void Overlay_FormClosing(object sender, FormClosingEventArgs e)
        {
            inOvrMode = false;
        }

        private void formClose_Click(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            this.Close();
        }

        private void formMaxi_Click(object sender, EventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized) { this.WindowState = FormWindowState.Maximized; }
            else { this.WindowState = FormWindowState.Normal; }
        }

        private void formMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void controlOpacity_Scroll(object sender, EventArgs e)
        {
            // Get the current opacity of the form/control
            var currentOpacity = this.Opacity;

            // Calculate the new opacity based on the scroll value (0 to 100)
            int scrollValue = controlOpacity.Value;
            float newOpacity = 1.0f - (scrollValue / 100.0f);

            // Set the new opacity for the form/control
            if (newOpacity < 0.10) { }
            else { this.Opacity = newOpacity; }

        }

        bool anti;
        private void antiCon_Click(object sender, EventArgs e)
        {
            if (anti == false) { MessageBox.Show("Status: On"); anti = true; }
            else { anti = false; MessageBox.Show("Status: Off"); }
        }
    }
}