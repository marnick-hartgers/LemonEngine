using System.Drawing;
using LemonEngine.Infrastructure.Render.Renderable.Model;
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
                gl.GenerateMipmapEXT(OpenGL.GL_TEXTURE_2D);
            }
        }

        public void Set(OpenGL gl)
        {

            if (HasTexture)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, _glTexture.TextureName);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_REPEAT);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR);
                gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR);
                gl.Color(1.0f, 1.0f, 1.0f);
            }
            else
            {
                gl.Color(DifColor.AsArray);
                gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, DifColor.AsArray);
                gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, DifColor.AsArray);
                gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, SpeColor.AsArray);
                gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, 90f);
            }
            

            //gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, AmbColor.AsArray);
            
        }

        public void Unset(OpenGL gl)
        {
            if (HasTexture)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);
            }
            gl.Color(0f,0f,0f,0f);
        }
    }
}
