using SharpGL;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Infrastructure.Render.Renderable
{
    public interface IRenderService
    {
        void Init(OpenGL gl);
        IRenderable AddRenderable(string model, string material);
        void Render(OpenGL gl);
        Vec3 SkyColor { get; set; }
    }
}
