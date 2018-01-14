using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using SharpGL.Shaders;
using LemonEngine.Infrastructure.Render.Shader;

namespace LemonEngine.RenderLogic.Shaders
{
    public class DefaultShader : IShader
    {
        private ShaderProgram _shaderProgram;
        public uint PositionAttributeIndex { get; }
        public uint AmbColorAttributeIndex { get; }
        public uint DifColorAttributeIndex { get; }
        public uint SpeColorAttributeIndex { get; }
        public uint NormalAttributeIndex { get; }

        public ShaderProgram ShaderProgram { get { return _shaderProgram; } }
        public DefaultShader()
        {
            PositionAttributeIndex = 0;
            AmbColorAttributeIndex = 1;
            DifColorAttributeIndex = 2;
            SpeColorAttributeIndex = 3;
            NormalAttributeIndex = 4;
            _shaderProgram = new ShaderProgram();
        }

        public void Create(OpenGL gl)
        {
            //gl.Enable(OpenGL.GL_);
            gl.EnableVertexAttribArray(0);
            gl.EnableVertexAttribArray(1);
            gl.EnableVertexAttribArray(2);
            gl.EnableVertexAttribArray(3);
            gl.EnableVertexAttribArray(4);
            _shaderProgram.Create(gl, vertexsource, fragmentsource, null);
            _shaderProgram.Bind(gl);
            int test = _shaderProgram.GetAttributeLocation(gl, "in_Normal");
            _shaderProgram.BindAttributeLocation(gl, PositionAttributeIndex, "in_Position");
            _shaderProgram.BindAttributeLocation(gl, AmbColorAttributeIndex, "in_AmbColor");
            _shaderProgram.BindAttributeLocation(gl, DifColorAttributeIndex, "in_DifColor");
            _shaderProgram.BindAttributeLocation(gl, SpeColorAttributeIndex, "in_SpeColor");
            _shaderProgram.BindAttributeLocation(gl, NormalAttributeIndex, "in_Normal");
            _shaderProgram.AssertValid(gl);
        }

        public void BindToGl(OpenGL gl)
        {
            _shaderProgram.Bind(gl);
        }

        public void UnbindToGl(OpenGL gl)
        {
            _shaderProgram.Unbind(gl);
        }
        private string vertexsource = @"
        #version 330 core

        layout(location = 0) in vec3 in_Position;
        layout(location = 1) in vec3 in_AmbColor;  
        layout(location = 2) in vec3 in_DifColor; 
        layout(location = 3) in vec3 in_SpeColor; 
        layout(location = 4) in vec3 in_Normal;  
        out vec4 pass_Color;
        uniform mat4 projectionMatrix;
        uniform mat4 viewMatrix;
        uniform mat4 modelMatrix;
    
        vec4 brightness;

        void main(void) {
	        gl_Position = projectionMatrix * viewMatrix * modelMatrix * vec4(in_Position, 1.0);
            brightness = (projectionMatrix * viewMatrix * modelMatrix * vec4(in_Normal, 1.0));
	        pass_Color = vec4(in_DifColor * ( (brightness.x + brightness.y + brightness.z) / 20), 1.0);
            //pass_Color = vec4(in_Normal , 1.0);
        }
";
        private string fragmentsource = @"
        #version 330 core
        in vec4 pass_Color;
        out vec4 out_Color;

        void main(void) {
	        out_Color = pass_Color;
        }
";
    }
}
