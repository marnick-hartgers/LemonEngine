using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Camera
{
    public interface ICamera
    {
        void SetCamera(OpenGL gl, IShader shader);
        void SetParameters(Vec3 position, Vec3 rotation);
        void SetAspectRatio(float x, float y);
        Vec3 Position { get; }
        Vec3 Rotation { get; }
    }
}