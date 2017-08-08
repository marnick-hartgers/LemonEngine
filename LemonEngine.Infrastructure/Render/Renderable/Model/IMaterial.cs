using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IMaterial
    {
        string Name { get; }

        void Init(OpenGL gl);
        void Set(OpenGL gl);
        void Unset(OpenGL gl);
    }
}
