using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using SharpGL.Shaders;

namespace LemonEngine.Infrastructure.Render.Shader
{
    public interface IShader
    {
        void Create(OpenGL gl);
        void BindToGl(OpenGL gl);
        void UnbindToGl(OpenGL gl);
        ShaderProgram ShaderProgram { get; }
        uint PositionAttributeIndex { get; }
        uint AmbColorAttributeIndex { get; }
        uint DifColorAttributeIndex { get; }
        uint SpeColorAttributeIndex { get; }
        uint NormalAttributeIndex { get; }
        uint TextCordsAttributeIndex { get; }
    }
}
