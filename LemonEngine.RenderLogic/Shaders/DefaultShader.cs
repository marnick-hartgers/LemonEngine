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
        public uint TextCordsAttributeIndex { get; }

        public ShaderProgram ShaderProgram { get { return _shaderProgram; } }
        public DefaultShader()
        {
            PositionAttributeIndex = 0;
            AmbColorAttributeIndex = 1;
            DifColorAttributeIndex = 2;
            SpeColorAttributeIndex = 3;
            NormalAttributeIndex = 4;
            TextCordsAttributeIndex = 5;
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
            gl.EnableVertexAttribArray(5);
            _shaderProgram.Create(gl, vertexsource, fragmentsource, null);
            _shaderProgram.Bind(gl);
            int test = _shaderProgram.GetAttributeLocation(gl, "in_Normal");
            _shaderProgram.BindAttributeLocation(gl, PositionAttributeIndex, "in_Position");
            _shaderProgram.BindAttributeLocation(gl, AmbColorAttributeIndex, "in_AmbColor");
            _shaderProgram.BindAttributeLocation(gl, DifColorAttributeIndex, "in_DifColor");
            _shaderProgram.BindAttributeLocation(gl, SpeColorAttributeIndex, "in_SpeColor");
            _shaderProgram.BindAttributeLocation(gl, NormalAttributeIndex, "in_Normal");
            _shaderProgram.BindAttributeLocation(gl, TextCordsAttributeIndex, "in_tex");
            _shaderProgram.SetUniform1(gl, "hasTex", 0);
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
        layout(location = 5) in vec2 in_tex;  
        out vec3 pass_AmbColor;
        out vec3 pass_DifColor;
        out vec3 pass_SpeColor;
        out vec3 pass_Normal;
        out mat3 pass_model;
        out vec3 pass_pos;
        out vec2 pass_tex;
        out mat3 pass_viewMatrix;
        out vec3 pass_lightDir;
        uniform mat4 projectionMatrix;
        uniform mat4 viewMatrix;
        uniform mat4 modelMatrix;
        uniform mat4 viewMatrixRotation;
        uniform float hasTex;

        void main(void) {
	        gl_Position = projectionMatrix * (viewMatrix * (modelMatrix * vec4(in_Position, 1.0)));
            pass_Normal = in_Normal;
            pass_model = mat3(modelMatrix );
            pass_viewMatrix = mat3(viewMatrix );
            pass_AmbColor = in_AmbColor;
            pass_DifColor = in_DifColor;
            pass_SpeColor = in_SpeColor;
            pass_pos = vec3(gl_Position);
            pass_lightDir = vec3(0,1,1) * mat3(viewMatrixRotation);
            if(hasTex == 1){
                pass_tex = in_tex;
            }
        }
";
        private string fragmentsource = @"
        #version 330 core
        in vec3 pass_AmbColor;
        in vec3 pass_DifColor;
        in vec3 pass_SpeColor;
        in vec3 pass_Normal;
        in mat3 pass_model;
        in vec3 pass_pos;
        in vec2 pass_tex;
        in mat3 pass_viewMatrix;
        in vec3 pass_lightDir;
        out vec4 out_Color;

        uniform sampler2D tex;
        uniform float hasTex;

        void main(void) {
            vec3 n = normalize(pass_model * pass_Normal) * pass_lightDir; 
            vec3 s = pass_lightDir;

            float sdn = max(dot(n, s), 0.0);
            
            vec3 v = normalize(pass_pos);
            vec3 r = reflect(-s, n);

            vec3 spec = vec3(0.0);
            
            spec = pow(max(dot(r, v), 0.0), 3.0) * pass_SpeColor;
            vec3 color = pass_AmbColor + pass_DifColor * (sdn + 0.15) + (0.15 * spec);
            
            if(hasTex == 1){
                out_Color = vec4(texture( tex, vec2(pass_tex.x, 1-pass_tex.y)).rgb * color , 1);
                //out_Color = vec4(texture( tex, vec2(pass_tex.x, 1-pass_tex.y)).rgb , 1);
                if(max(max(out_Color.x,out_Color.y),out_Color.z) == 0){
                    discard;
                }
            }else{
                out_Color = vec4(color,1.0);
                
            }
            
	        //out_Color = vec4(pass_tex, 0.0,1.0);
        }
";
    }
}
