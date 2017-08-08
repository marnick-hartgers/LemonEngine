using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IMaterialRepository
    {
        void Load(OpenGL gl);
        IMaterialGroup GetMaterialGroup(string groupname);

        IMaterialGroup[] MaterialGroups { get; }
    }
}
