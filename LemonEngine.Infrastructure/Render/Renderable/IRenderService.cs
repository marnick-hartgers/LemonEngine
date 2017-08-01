using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Render.Renderable
{
    public interface IRenderService
    {
        void Init();
        IRenderable AddRenderable(string model, string material);
        void Render(OpenGL gl);
    }
}
