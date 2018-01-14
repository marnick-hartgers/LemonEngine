using LemonEngine.Infrastructure.Render.Shader;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Camera
{
    public interface ICamera
    {
        void SetCamera(OpenGL gl, IShader shader);
    }
}