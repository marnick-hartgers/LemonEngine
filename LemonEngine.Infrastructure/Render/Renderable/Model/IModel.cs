using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModel
    {
        string Name { get; set; }
        string MaterialGroup { get; set; }
        List<IModelPart> Parts { get; }
        void Draw(OpenGL gl, ICamera camera);
        void BindToGl(OpenGL gl);
        void SetParameters(Vec3 position, Vec3 rotation);
    }
}
