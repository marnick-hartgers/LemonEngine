using System;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.RenderLogic.Events;
using SharpGL;
using SharpGL.Enumerations;

namespace LemonEngine.RenderLogic
{

    public class RenderEnigne
    {
        public EventHandler OnLoadDone;
        public EventHandler<ResizedEventArgs> OnResized;

        private RenderService _renderService;

        private RenderSettings _renderSettings;
        public void StartLoad(OpenGL gl)
        {
            _renderSettings = new RenderSettings();

            gl.Enable(OpenGL.GL_BLEND);
            gl.ShadeModel(OpenGL.GL_SMOOTH);

            gl.Enable(OpenGL.GL_CULL_FACE);
            gl.CullFace(OpenGL.GL_BACK);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.DepthFunc(OpenGL.GL_LEQUAL);
            gl.Enable(OpenGL.GL_DEPTH_TEST);

            _renderService = new RenderService();
            _renderService.Init(gl);



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
            
        }

        public void AddLight(ILight light)
        {
        }
        

        public void SetResolution(int x, int y)
        {
            OnResized?.Invoke(this, new ResizedEventArgs(x,y));
        }
    }

    public class RenderSettings
    {
        public bool UseLight = true;
        public bool UseSmooth = true;
        public bool UseColorMaterials = false;
        public bool UseTextures = true;
        public bool HasPendingChanges = false;
    }
}
