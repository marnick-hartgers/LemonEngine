using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Render.Renderable.Model;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic.Renderables.Model;
using SharpGL;

namespace LemonEngine.RenderLogic.Renderables
{
    public class Renderable : IRenderable
    {
        public Renderable(string modelname, string materialname)
        {
            Model = ModelRepository.GetInstance().GetModelByName(modelname);
            Position = new Vec3(0,0,0);
            Rotation = new Vec3(0, 0, 0);
            Scale = new Vec3(1, 1, 1);
        }

        public void InitEntity(OpenGL gl)
        {
            
        }

        public void DrawEntity(OpenGL gl)
        {
            //texture enzo
            gl.Translate(Position.X, Position.Y, Position.Z);
            gl.Rotate(Rotation.X, Rotation.Y, Rotation.Z);
            
            for (int p = 0; p < Model.Parts.Count; p++)
            {
                Model.DrawPart(Model.Parts[p],gl);
            }

            gl.Rotate(-Rotation.X, -Rotation.Y, -Rotation.Z);
            gl.Translate(-Position.X, -Position.Y, -Position.Z);
        }

        public IMaterial Material { get; }
        public IModel Model { get; }
        public Vec3 Position { get; }
        public Vec3 Rotation { get; }
        public Vec3 Scale { get; }
    }
}
