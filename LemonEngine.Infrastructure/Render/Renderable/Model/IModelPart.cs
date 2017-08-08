using System.Collections.Generic;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelPart
    {
        string Name { get; set; }
        List<IModelPartFace> Faces { get; }
    }


}
