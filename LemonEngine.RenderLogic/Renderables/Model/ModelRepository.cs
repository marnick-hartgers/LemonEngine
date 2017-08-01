using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.RenderLogic.ModelLoader;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class ModelRepository : IModelRepository
    {
        private ObjFileReader modelReader = new ObjFileReader();

        public void StartLoad()
        {
            var res = modelReader.ReadFolder();

        }

        public IModel GetModelByName(string name)
        {
            return null;
        }
    }
}
