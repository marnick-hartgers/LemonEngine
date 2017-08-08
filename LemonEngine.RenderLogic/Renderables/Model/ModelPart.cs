using System.Collections.Generic;
using LemonEngine.Infrastructure.Render.Renderable.Model;

namespace LemonEngine.RenderLogic.Renderables.Model
{
    public class ModelPart : IModelPart
    {
        public string Name { get; set; }
        

        public List<IModelPartFace> Faces { get; }

        public ModelPart()
        {
            Faces = new List<IModelPartFace>();
        }

    }
}