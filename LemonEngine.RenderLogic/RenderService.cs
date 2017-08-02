using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.RenderLogic.Renderables;
using LemonEngine.RenderLogic.Renderables.Model;
using SharpGL;

namespace LemonEngine.RenderLogic
{
    public class RenderService : IRenderService
    {

        private List<Renderable> _renderables = new List<Renderable>();
        private IModelRepository _modelRepository;
        private int _renderIndex = 0;

        public void Init()
        {
            _modelRepository = ModelRepository.GetInstance();
            _modelRepository.StartLoad();
        }

        public IRenderable AddRenderable(string model, string material)
        {
            var r = new Renderable(model, material);
            _renderables.Add(r);
            return r;
        }


        public void Render(OpenGL gl)
        {

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //St matrix mode to PROJECTION so we can move the camera
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity matrix.
            gl.LoadIdentity();
            //Move the camera
            gl.Perspective(60.0f, 800f / 600f, 0.01, 1000.0);

            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-205, 0, 100, 50, 0, 0, 0, 0, 1);
            //Set matrix mode back to Modelview so we can draw objects
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            _renderIndex = 0;
            while (_renderIndex < _renderables.Count)
            {
                
                _renderables[_renderIndex].DrawEntity(gl);
                _renderIndex++;
            }

            gl.Flush();


        }
    }
}
