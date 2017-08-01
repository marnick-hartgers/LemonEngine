using SharpGL;

namespace LemonEngine.Infrastructure.Render.Renderable.Model
{
    public interface IMaterialRepository
    {
        void Load(OpenGL gl);
        IMaterial GetMaterialById();
        IMaterial GetMaterialByName();
    }
}
