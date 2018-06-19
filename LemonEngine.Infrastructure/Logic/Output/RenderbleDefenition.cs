using LemonEngine.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Logic.Output
{
    public class RenderbleDefenition
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public Vec3 Position { get; set; }
        public Vec3 PositionDelta { get; set; }
        public Vec3 Rotation { get; set; }
        public Vec3 RotationDelta { get; set; }
        public Vec3 Scale { get; set;  }
    }
}
