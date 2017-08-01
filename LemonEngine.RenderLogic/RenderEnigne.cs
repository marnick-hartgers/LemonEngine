using System;
using LemonEngine.Infrastructure.Render.Renderable;
using SharpGL;

namespace LemonEngine.RenderLogic
{

    public class RenderEnigne
    {
        public EventHandler OnLoadDone;

        private RenderService _renderService;

        public void StartLoad()
        {
            _renderService = new RenderService();
            _renderService.Init();
            SignalLoadDone();
        }

        private void SignalLoadDone()
        {
            OnLoadDone?.Invoke(this, EventArgs.Empty);
        }

        public void AddRenderable(IRenderable renderable)
        {

        }

        public void Render(OpenGL gl)
        {

        }
    }
}
