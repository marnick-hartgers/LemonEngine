using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.RenderLogic;
using LemonEngine.RenderLogic.Light;
using SharpGL;
using PInvoke;

namespace WallpaperGrabber
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            _renderEngine = new RenderEnigne();
            InitializeComponent();
            Load += Form1_Load; 
            InitOpenGl();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetParent();
        }

        [DllImport("User32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]        
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void SetParent()
        {
            IntPtr progman = User32.FindWindow("Progman",null);
            SendMessage(progman, 0x052C, new IntPtr(0), IntPtr.Zero);
            IntPtr workerW = IntPtr.Zero;

            User32.EnumWindows((hwnd, param) =>
            {
                if (User32.FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                {
                    workerW = User32.FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);
                }
                return true;
            }, IntPtr.Zero);
            if (workerW != IntPtr.Zero)
            {
                User32.SetParent(Handle, workerW);
            }
            
        }


        private OpenGLWallpaperControl openGLControl = null;

        private RenderEnigne _renderEngine;

        private IRenderable testDing;

        private void InitOpenGl()
        {
            openGLControl = new OpenGLWallpaperControl();
            ((ISupportInitialize)(openGLControl)).BeginInit();
            SuspendLayout();
            // 
            // openGLControl
            // 
            openGLControl.Dock = DockStyle.Fill;
            openGLControl.DrawFPS = true;
            openGLControl.FrameRate = 20;
            openGLControl.Location = new Point(0, 0);
            openGLControl.Name = "openGLControl";
            openGLControl.RenderContextType = RenderContextType.NativeWindow;
            openGLControl.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            openGLControl.TabIndex = 0;
            //openGLControl.OpenGLVersion = OpenGLVersion.OpenGL3_0;
            openGLControl.OpenGLInitialized += openGLControl_OpenGLInitialized;
            openGLControl.OpenGLDraw += openGLControl_OpenGLDraw;
            openGLControl.Resized += openGLControl_Resized;
            // 
            // SharpGLForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Controls.Add(openGLControl);
            Name = "SharpGLForm";
            Text = "SharpGL Form";
            ((ISupportInitialize)(openGLControl)).EndInit();
            ResumeLayout(false);
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            _renderEngine.StartLoad(openGLControl.OpenGL);
            //testDing = _renderEngine.AddRenderable("RmhDktMako", "");

            testDing = _renderEngine.AddRenderable("low-poly-mill", "");
            //testDing = _renderEngine.AddRenderable("lp", "");

            var l = new Light(0);
            l.Position.X = -1;
            l.Position.Y = -1;
            l.Position.Z = 1;
            l.Init(openGLControl.OpenGL);
            

            _renderEngine.AddLight(l);

        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {

        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            testDing.Rotation.Z =(DateTime.Now.Second + DateTime.Now.Millisecond / 1000f) / 60f * 360f;
            _renderEngine.Render(openGLControl.OpenGL);
        }

    }
}
