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
        public RenderService RenderService => _renderService;
        
        public void StartLoad(OpenGL gl)
        {
            _renderService = new RenderService();
            _renderService.Init(gl);
            SignalLoadDone();
        }

        private void SignalLoadDone()
        {
            OnLoadDone?.Invoke(this, EventArgs.Empty);
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
            _renderService.SetAspectRatio((float)x, (float)y);
            OnResized?.Invoke(this, new ResizedEventArgs(x,y));
        }
    }
}
