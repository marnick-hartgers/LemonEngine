using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Settings;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Logic.Context
{
    public class GraphicsContext : IGraphicsContext
    {
        private bool _changed = false;

        private Vec3 _clearColor = new Vec3();
        public void SetClearColor(Vec3 color)
        {
            _changed = true;
            _clearColor = color;
        }

        public void Sync(IRenderService renderService)
        {
            if (_changed)
            {
                RenderSettings renderSettings = new RenderSettings();
                renderSettings.ClearColor = new Vec3(_clearColor);
                renderService.SetRenderSettings(renderSettings);
            }
            
        }
    }
}
