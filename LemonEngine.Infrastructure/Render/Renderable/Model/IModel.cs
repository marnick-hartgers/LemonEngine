using LemonEngine.Infrastructure.Types.Render;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModel
    {
        string Name { get; }
        void Draw(OpenGL opengl);
    }
}
