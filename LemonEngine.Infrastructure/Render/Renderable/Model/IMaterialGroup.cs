using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IMaterialGroup
    {
        string Name { get; }
        IMaterial[] Materials { get; }
    }
}
