using SharpGL;
using LemonEngine.Infrastructure.Render.Light;

namespace LemonEngine.Infrastructure.Render.Renderable
{
    public interface IRenderService
    {
        void Init(OpenGL gl);
        IRenderable AddRenderable(string model, string material);
        void Render(OpenGL gl);

        void AddLight(ILight light);
    }
}
