using LemonEngine.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Render.Settings
{
    public class RenderSettings
    {
        public Vec3 ClearColor { get; set; }


        public static readonly RenderSettings Empty = new RenderSettings { ClearColor = new Vec3(0, 0, 0) };
    }
}
