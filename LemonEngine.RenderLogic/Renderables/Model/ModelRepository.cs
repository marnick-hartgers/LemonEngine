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

        private Model[] models = null;

        private static ModelRepository _singleton;

        public static ModelRepository GetInstance()
        {
            return _singleton ?? (_singleton = new ModelRepository());
        }

        public void StartLoad()
        {
            var res = modelReader.ReadFolder();
            models = res.ToArray();
        }

        public IModel GetModelByName(string name)
        {
            return models.First((m) => (m.Name == name));
        }
    }
}
