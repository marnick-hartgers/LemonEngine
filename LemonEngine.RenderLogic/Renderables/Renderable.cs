using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
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
            MaterialGroup = MaterialRepository.GetInstance().GetMaterialGroup(Model.MaterialGroup);
            Position = new Vec3(0, 0, 0);
            Rotation = new Vec3(0, 0, 0);
            Scale = new Vec3(1, 1, 1);
        }

        public void InitEntity(OpenGL gl)
        {

        }

        public void DrawEntity(OpenGL gl)
        {
            //gl.PushMatrix();
            gl.MatrixMode(MatrixMode.Modelview);
            //gl.Translate(Position.X, Position.Y, Position.Z);
            gl.Rotate(Rotation.X, Rotation.Y, Rotation.Z);
            for (int p = 0; p < Model.Parts.Count; p++)
            {
                Model.DrawPart(Model.Parts[p], MaterialGroup, gl);
            }
            //gl.PopMatrix();
        }

        public IMaterialGroup MaterialGroup { get; }
        public IModel Model { get; }
        public Vec3 Position { get; }
        public Vec3 Rotation { get; }
        public Vec3 Scale { get; }
    }
}
