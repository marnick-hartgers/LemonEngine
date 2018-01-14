using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using SharpGL;
using SharpGL.VertexBuffers;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelPart
    {
        int VertexCount { get; }
        void BindToGl(OpenGL gl, IShader shader);
        void BindForDraw(OpenGL gl, IShader shader);
        void UnbindForDraw(OpenGL gl, IShader shader);
    }


}
