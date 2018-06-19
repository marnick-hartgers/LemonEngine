using LemonEngine.Infrastructure.Logic.Output;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Settings;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using SharpGL;
using System;

namespace LemonEngine.Infrastructure.Render.Renderable
{
    //Minimum requirements for drawing something
    public interface IRenderable
    {
        void InitEntity(OpenGL gl);
        void DrawEntity(OpenGL gl, ICamera camera, RenderSettings renderSettings);
        IModel Model { get; }
        Vec3 Position { get; }
        Vec3 Rotation { get; }
        Vec3 Scale { get; }
        Guid Id { get; }

        void SyncFromDefenition(RenderbleDefenition rDef);
    }
}
