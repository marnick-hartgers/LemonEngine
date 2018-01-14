using LemonEngine.Infrastructure.Types;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IMaterial
    {
        string Name { get; }

        void Init(OpenGL gl);
        void Set(OpenGL gl);
        void Unset(OpenGL gl);
        Vec3 AmbColor { get; }
        Vec3 DifColor { get; }
        Vec3 SpeColor { get; }
        Vec3 Color { get; set; }
    }
}
