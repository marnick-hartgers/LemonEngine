using System;
using LemonEngine.Infrastructure.Render.Renderable;
using SharpGL;

namespace LemonEngine.RenderLogic
{

    public class RenderEnigne
    {
        public EventHandler OnLoadDone;

        private RenderService _renderService;

        public void StartLoad(OpenGL gl)
        {
            gl.Enable(OpenGL.GL_BLEND);
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            //fffgl.Enable(OpenGL.GL_LIGHTING);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.ShadeModel(OpenGL.GL_SMOOTH);
            _renderService = new RenderService();
            _renderService.Init();
            SignalLoadDone();
        }

        private void SignalLoadDone()
        {
            OnLoadDone?.Invoke(this, EventArgs.Empty);
        }

        public IRenderable AddRenderable(string model, string material)
        {
            return _renderService.AddRenderable(model, material);
        }

        public void Render(OpenGL gl)
        {
            _renderService.Render(gl);
            gl.Flush();
        }
    }
}
