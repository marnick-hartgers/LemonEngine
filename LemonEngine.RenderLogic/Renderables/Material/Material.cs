using System.Drawing;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace LemonEngine.RenderLogic.Renderables.Material
{
    public class Material : IMaterial
    {

        public Material(string name)
        {
            Name = name.Trim();
        }

        public string Name { get; }

        public Vec3 Color { get; set; }

        public Vec3 AmbColor { get; set; }
        public Vec3 DifColor { get; set; }
        public Vec3 SpeColor { get; set; }
        public float Illum { get; set; }

        private Texture _glTexture = null;
        public Bitmap Texture { get; set; }


        public bool HasTexture { get; set; }

        public void Init(OpenGL gl)
        {
            if (HasTexture)
            {
                _glTexture = new Texture();
                _glTexture.Create(gl, Texture);
                //gl.GenerateMipmapEXT(OpenGL.GL_TEXTURE_2D);
            }
        }

        public void Set(OpenGL gl, IShader shader)
        {

            if (HasTexture)
            {

                _glTexture.Bind(gl);
                shader.ShaderProgram.SetUniform1(gl, "tex", _glTexture.TextureName);
                shader.ShaderProgram.SetUniform1(gl, "hasTex", 1);

            }
            else
            {
                shader.ShaderProgram.SetUniform1(gl, "hasTex", 0);
            }
            
        }

        public void Unset(OpenGL gl)
        {
            if (HasTexture)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);

            }
        }
    }
}
