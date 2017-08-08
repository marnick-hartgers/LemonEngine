using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.RenderLogic.Renderables;
using LemonEngine.RenderLogic.Renderables.Material;
using LemonEngine.RenderLogic.Renderables.Model;
using SharpGL;

namespace LemonEngine.RenderLogic
{
    public class RenderService : IRenderService
    {

        private readonly List<Renderable> _renderables = new List<Renderable>();
        private IModelRepository _modelRepository;
        private IMaterialRepository _materialRepository;
        private int _renderIndex = 0;

        private readonly List<ILight> _lights = new List<ILight>();

        public void Init(OpenGL gl)
        {
            _materialRepository = MaterialRepository.GetInstance();
            _materialRepository.Load(gl);
            _modelRepository = ModelRepository.GetInstance();
            _modelRepository.StartLoad(_materialRepository);

            
            foreach (var materialGroup in _materialRepository.MaterialGroups)
            {
                foreach (var material in materialGroup.Materials)
                {
                    material.Init(gl);
                }
            }
        }

        public IRenderable AddRenderable(string model, string material)
        {
            var r = new Renderable(model);
            _renderables.Add(r);
            return r;
        }


        public void Render(OpenGL gl)
        {
            
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(60.0f, 800f / 600f, 0.01, 10000.0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.LookAt(-1.5, -1.5, 1.0, 0, 0, 0, 0, 0, 1);
            
            _renderIndex = 0;
            while (_renderIndex < _renderables.Count)
            {

                _renderables[_renderIndex].DrawEntity(gl);
                _renderIndex++;
            }
            foreach (var light in _lights)
            {
                light.Draw(gl);
            }

        }

        public void AddLight(ILight light)
        {
            _lights.Add(light);
        }
    }
}
