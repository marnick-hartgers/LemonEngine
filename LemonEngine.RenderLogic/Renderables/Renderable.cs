using LemonEngine.Infrastructure.Logic.Output;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Render.Settings;
using LemonEngine.Infrastructure.Render.Shader;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables.Material;
using LemonEngine.RenderLogic.Renderables.Model;
using SharpGL;
using SharpGL.Enumerations;
using System;

namespace LemonEngine.RenderLogic.Renderables
{
    public class Renderable : IRenderable
    {
        public Renderable(string modelname, Guid id)
        {
            Model = ModelRepository.GetInstance().GetModelByName(modelname);
            //MaterialGroup = MaterialRepository.GetInstance().GetMaterialGroup(Model.MaterialGroup);
            Position = new Vec3(0, 0, 0);
            Rotation = new Vec3(0, 0, 0);
            Scale = new Vec3(1, 1, 1);
            Id = id;
        }

        public void InitEntity(OpenGL gl)
        {
        }

        public void DrawEntity(OpenGL gl, ICamera camera, RenderSettings renderSettings)
        {
            Model.SetParameters(Position, Rotation);
            Model.Draw(gl,camera);
        }

        public void SyncFromDefenition(RenderbleDefenition rDef)
        {
            if (Model.Name != rDef.ModelName)
            {
                Model = ModelRepository.GetInstance().GetModelByName(rDef.ModelName);
            }
            Vec3.Copy(rDef.Position, Position);
            Vec3.Copy(rDef.Rotation, Rotation);
            Vec3.Copy(rDef.Scale, Scale);
        }

        public IModel Model { get; private set; }
        public Vec3 Position { get; }
        public Vec3 Rotation { get; }
        public Vec3 Scale { get; }
        public Guid Id { get; }
    }
}
