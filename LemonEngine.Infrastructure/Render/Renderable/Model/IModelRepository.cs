using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IModelRepository
    {
        void StartLoad();
        IModel GetModelByName(string name);
    }
}
