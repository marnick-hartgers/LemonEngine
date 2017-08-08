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

        public Vec3 Ambient { get; set; }
        public Vec3 Diffuse { get; set; }
        public Vec3 Specular { get; set; }
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
                
            }
        }

        public void Set(OpenGL gl)
        {

            if (HasTexture)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, _glTexture.TextureName);
            }
            gl.Color(Diffuse.AsArray);

            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, Ambient.AsArray);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, Diffuse.AsArray);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, Specular.AsArray);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, 50f);
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
