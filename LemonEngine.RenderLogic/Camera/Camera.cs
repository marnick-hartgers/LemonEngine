using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.RenderLogic.Camera
{
    public class Camera
    {
        public Camera()
        {
            Position = new Vec3();
            Rotation = new Vec3();
        }
        public Vec3 Position { get; }
        public Vec3 Rotation { get; }
    }
}
