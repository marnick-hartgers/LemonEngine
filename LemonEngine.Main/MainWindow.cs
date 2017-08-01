using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using LemonEngine.RenderLogic;
using SharpGL;


namespace LemonEngine.Main
{
    public partial class MainWindow : Form
    {
        private OpenGLControl openGLControl = null;

        private RenderEnigne _renderEngine;

        public MainWindow()
        {
            //InitializeComponent();

            _renderEngine = new RenderEnigne();

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
            openGLControl.FrameRate = 20;
            openGLControl.Location = new Point(0, 0);
            openGLControl.Name = "openGLControl";
            openGLControl.RenderContextType = RenderContextType.NativeWindow;
            openGLControl.Size = new Size(624, 391);
            openGLControl.TabIndex = 0;
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
            _renderEngine.StartLoad();
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            _renderEngine.Render(openGLControl.OpenGL);
        }
    }
}
