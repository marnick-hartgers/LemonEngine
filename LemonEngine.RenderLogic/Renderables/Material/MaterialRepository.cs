using System.Linq;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.RenderLogic.MaterialLoader;
using SharpGL;

namespace LemonEngine.RenderLogic.Renderables.Material
{
    public class MaterialRepository : IMaterialRepository
    {
        private MtlFileReader mtlReader = new MtlFileReader();
        public IMaterialGroup[] MaterialGroups { get; private set; }

        private static MaterialRepository _singleton;

        public static MaterialRepository GetInstance()
        {
            return _singleton ?? (_singleton = new MaterialRepository());
        }

    public void Load(OpenGL gl)
    {
        var res = mtlReader.ReadFolder();
        MaterialGroups = res.ToArray();
    }

    public IMaterialGroup GetMaterialGroup(string groupname)
    {
        return MaterialGroups.FirstOrDefault(m => m.Name.Trim() == groupname.Trim());
    }
}
}
