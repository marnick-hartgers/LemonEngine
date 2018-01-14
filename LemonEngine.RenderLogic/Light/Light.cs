using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Types;
using SharpGL;
using SharpGL.Enumerations;

namespace LemonEngine.RenderLogic.Light
{
    public class Light : ILight
    {
        private int _lightNumber;
        private bool _update;
        public Light(int lightNumber)
        {
            Position = new Vec3(0, 0, 0);
            _lightNumber = lightNumber;
            _update = true;
        }

        public void Init(OpenGL gl)
        {
            //gl.Enable(OpenGL.GL_LIGHT0);
        }

        public void Draw(OpenGL gl)
        {
            if (_update)
            {
                Setlight(gl);
                _update = false;
            }
        }

        private void Setlight(OpenGL gl)
        {
            return;
            gl.MatrixMode(MatrixMode.Projection);
            
            gl.PushMatrix();
            gl.LoadIdentity();
            
            gl.Light(LightName.Light0, LightParameter.Position, new float[] { -3f, -0f, 10f, 1f });
            gl.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.1f, 0.1f, 0.1f });
            gl.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1f, 1f, 1f, 1f });
            gl.Light(LightName.Light0, LightParameter.Specular, new float[] { 1f, 1f, 1f, 1f });
            gl.Light(LightName.Light0, LightParameter.SpotCutoff, 180.0f);
            gl.PopMatrix();
            gl.MatrixMode(MatrixMode.Modelview);
        }

        public Vec3 Position { get; }
    }
}
