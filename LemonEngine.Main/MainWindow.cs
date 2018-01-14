using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.RenderLogic;
using LemonEngine.RenderLogic.Light;
using SharpGL;
using SharpGL.Version;


namespace LemonEngine.Main
{
    public partial class MainWindow : Form
    {
        private OpenGLControl openGLControl = null;

        private RenderEnigne _renderEngine;

        private IRenderable testDing;

        public MainWindow()
        {
            //InitializeComponent();

            _renderEngine = new RenderEnigne();
            InitializeComponent();
            InitOpenGl();
            
            
        }

        private void InitOpenGl()
        {
            openGLControl = new OpenGLControl();
            ((ISupportInitialize)(openGLControl)).BeginInit();
            SuspendLayout();
            // 
            // openGLControl
            // 
            openGLControl.Dock = DockStyle.Fill;
            openGLControl.DrawFPS = true;
            openGLControl.FrameRate = 60;
            openGLControl.Location = new Point(0, 0);
            openGLControl.Name = "openGLControl";
            openGLControl.RenderContextType = RenderContextType.NativeWindow;
            openGLControl.Size = new Size(800, 600);
            openGLControl.TabIndex = 0;
            openGLControl.OpenGLVersion = OpenGLVersion.OpenGL4_4;
            openGLControl.OpenGLInitialized += openGLControl_OpenGLInitialized;
            openGLControl.OpenGLDraw += openGLControl_OpenGLDraw;
            openGLControl.Resized += openGLControl_Resized;
            // 
            // SharpGLForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 391);
            Controls.Add(openGLControl);
            Name = "SharpGLForm";
            Text = "SharpGL Form";
            ((ISupportInitialize)(openGLControl)).EndInit();
            ResumeLayout(false);
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            _renderEngine.StartLoad(openGLControl.OpenGL);
            testDing = _renderEngine.AddRenderable("LP_001", "");
            //testDing = _renderEngine.AddRenderable("low-poly-mill", "");

            var l = new Light(0);
            l.Position.X = 1;
            l.Position.Y = 1;
            l.Position.Z = 1;
            l.Init(openGLControl.OpenGL);

            
            _renderEngine.AddLight(l);

        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            _renderEngine.Render(openGLControl.OpenGL);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            testDing.Rotation.X = (float) trackBar1.Value/50;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            testDing.Rotation.Y = (float)trackBar2.Value / 50;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            testDing.Rotation.Z = (float)trackBar3.Value / 50;
        }
    }
}
