using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IMaterial
    {
        int MaterialId { get; }
        void Load(OpenGL gl);
    }
}
