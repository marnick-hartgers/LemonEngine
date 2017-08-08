using System.Linq;
using LemonEngine.Infrastructure.Render.Renderable.Model;

namespace LemonEngine.RenderLogic.Renderables.Material
{
    public class MaterialGroup : IMaterialGroup
    {
        public MaterialGroup(string name, IMaterial[] materials)
        {
            Name = name.Trim();
            Materials = materials;
        }

        public string Name { get; }
        public IMaterial[] Materials { get; }
        public IMaterial GetMaterialByName(string name)
        {
            return Materials.FirstOrDefault(m => (m.Name == name));
        }
    }
}
