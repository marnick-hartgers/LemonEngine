using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable
{
    //Minimum requirements for drawing something
    public interface IRenderable
    {
        void InitEntity(OpenGL gl);
        void DrawEntity(OpenGL gl);
        IMaterial Material { get; }
        IModel Model { get; }
        Vec3 Position { get; }
        Vec3 Rotation { get; }
        Vec3 Scale { get; }

    }
}
