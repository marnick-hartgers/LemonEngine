using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Light;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables;
using LemonEngine.RenderLogic.Renderables.Material;
using LemonEngine.RenderLogic.Renderables.Model;
using LemonEngine.RenderLogic.Shaders;
using SharpGL;

namespace LemonEngine.RenderLogic
{
    public class RenderService : IRenderService
    {

        private readonly List<Renderable> _renderables = new List<Renderable>();
        private IModelRepository _modelRepository;
        private IMaterialRepository _materialRepository;
        private int _renderIndex = 0;




        public readonly Camera.Camera Camera = new Camera.Camera();

        private readonly List<ILight> _lights = new List<ILight>();


        public void Init(OpenGL gl)
        {
            _materialRepository = MaterialRepository.GetInstance();
            _materialRepository.Load(gl);
            _modelRepository = ModelRepository.GetInstance();
            _modelRepository.StartLoad(_materialRepository);
            _modelRepository.BindAll(gl);
            Camera.Position.X = 0;
            Camera.Position.Y = 0;
            Camera.Position.Z = -20;

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
            gl.ClearColor(0.5f, 0.7f, 1f, 0f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            //gl.LoadIdentity();
            _renderIndex = 0;
            while (_renderIndex < _renderables.Count)
            {
                _renderables[_renderIndex].DrawEntity(gl, Camera);
                _renderIndex++;
            }
            gl.Flush();
        }

    }
}
