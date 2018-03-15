using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables.Material;
using LemonEngine.RenderLogic.Renderables.Model;
using SharpGL;
using SharpGL.Enumerations;

namespace LemonEngine.RenderLogic.Renderables
{
    public class Renderable : IRenderable
    {
        public Renderable(string modelname)
        {
            Model = ModelRepository.GetInstance().GetModelByName(modelname);
            //MaterialGroup = MaterialRepository.GetInstance().GetMaterialGroup(Model.MaterialGroup);
            Position = new Vec3(0, 0, 0);
            Rotation = new Vec3(0, 0, 0);
            Scale = new Vec3(1, 1, 1);
        }

        public void InitEntity(OpenGL gl)
        {
        }

        public void DrawEntity(OpenGL gl, ICamera camera)
        {
            Model.SetParameters(Position, Rotation);
            Model.Draw(gl,camera);
        }

        public IMaterialGroup MaterialGroup { get; }
        public IModel Model { get; }
        public Vec3 Position { get; }
        public Vec3 Rotation { get; }
        public Vec3 Scale { get; }
    }
}
