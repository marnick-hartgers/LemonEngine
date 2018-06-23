using SharpGL;
using SharpGL.Shaders;
using System;
using System.Collections.Generic;

namespace LemonEngine.RenderLogic.Shaders.Programs
{
    public class FrameBufferTextureProgram
    {
        private ShaderProgram _shaderProgram;
        
        public void Init(OpenGL gl)
        {
            _shaderProgram = new ShaderProgram();
            Dictionary<uint, string> programParameters = new Dictionary<uint, string>();
            //gl.EnableVertexAttribArray(0);
            programParameters.Add(0, "aPos");
            //programParameters.Add(1, "aTexCoords");
            _shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            _shaderProgram.Bind(gl);
            
            _shaderProgram.BindAttributeLocation(gl, 0, "in_Position");
            _shaderProgram.BindAttributeLocation(gl, 1, "aTexCoords");

            _shaderProgram.AssertValid(gl);
        }

        public void Bind(OpenGL gl, uint tex)
        {
            _shaderProgram.Bind(gl);
            _shaderProgram.SetUniform1(gl, "screenTexture", tex);
        }

        public void Unbind(OpenGL gl)
        {
            _shaderProgram.Unbind(gl);
        }

        private string vertexShaderSource = @"
           #version 330 core
            layout (location = 0) in vec3 in_Position;
            layout (location = 1) in vec2 aTexCoords;

            out vec2 TexCoords;

            void main()
            {
                gl_Position = vec4(in_Position.x, in_Position.y, 0.0, 1.0); 
                TexCoords = vec2(aTexCoords.x, aTexCoords.y );
            }  
";

        private string fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;
  
            in vec2 TexCoords;

            uniform sampler2D screenTexture;

            const int quality = 10;
            const float offset = 1.0 / 800.0;
            const float halfTotal = offset * quality / 2;

            void main()
            { 
                vec3 sampleTex[9];
                vec3 col = vec3(0.0);
                vec3 org = vec3(texture(screenTexture, TexCoords));
                float count = 1;
                for(int i = 0; i < (quality * quality); i++)
                {
                    vec3 sample = vec3(texture(screenTexture, TexCoords + vec2(
                    -halfTotal + offset * (i % quality),
                    -halfTotal + offset * floor(i / quality)
                    )));
                    col = col + sample * 1.5;
                    count++;
                }
                col /= count;
                
                
                org = max(org, org * col * 2.0);//+ (col * col * col);
                FragColor = vec4( org, 1.0);
            }
";
    }
}
