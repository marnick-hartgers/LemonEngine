using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LemonEngine.RenderLogic;
using SharpGL;
using SharpGL.Version;

namespace GameWindow
{
    public abstract partial class GameWindow : Form
    {
        private OpenGLControl openGLControl = null;

        private RenderEnigne _renderEngine;
        public RenderService RenderService => _renderEngine.RenderService;

        public GameWindow(string name)
        {
            _renderEngine = new RenderEnigne();
            InitializeComponent();
            InitOpenGl(name);

        }

        private void InitOpenGl(string name)
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
            openGLControl.OpenGLVersion = OpenGLVersion.OpenGL4_0;
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
            Name = name;
            Text = name;
            ((ISupportInitialize)(openGLControl)).EndInit();
            ResumeLayout(false);
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            _renderEngine.StartLoad(openGLControl.OpenGL);
            OnInit();
        }
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            _renderEngine.SetResolution(Width, Height);
            
        }
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OnDraw();
            _renderEngine.Render(openGLControl.OpenGL);
            CheckMouseMovement();
        }

        private void CheckMouseMovement()
        {
            if (!ContainsFocus)
            {
                return;
            }
            int centerX = this.Location.X + (this.Size.Width / 2);
            int centerY = this.Location.Y + (this.Size.Height / 2);

            OnMouseMovement(Cursor.Position.X - centerX, Cursor.Position.Y - centerY);

            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(centerX, centerY);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
        }

        protected virtual void OnInit() { }

        protected virtual void OnDraw() { }

        protected virtual void OnMouseMovement(int x, int y) { }
    }
}
