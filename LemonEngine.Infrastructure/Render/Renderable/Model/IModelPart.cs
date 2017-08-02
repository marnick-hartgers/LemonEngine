using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelPart
    {
        string Name { get; set; }
        string MaterialName { get; set; }
        List<IModelPartFace> Faces { get; }
    }


}
