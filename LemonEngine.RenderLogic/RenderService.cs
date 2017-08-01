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


        public void Init()
        {
            _modelRepository = new ModelRepository();
            _modelRepository.StartLoad();
        }

        public IRenderable AddRenderable(string model, string material)
        {
            throw new System.NotImplementedException();
        }


        public void Render(OpenGL gl)
        {

        }
    }
}
