using LemonEngine.Infrastructure.Types;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Light
{
    public interface ILight
    {
        void Draw(OpenGL gl);
        void Init(OpenGL gl);
        Vec3 Position { get; }
    }
}
