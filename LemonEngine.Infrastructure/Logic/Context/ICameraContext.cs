using LemonEngine.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public interface ICameraContext
    {
        Vec3 Position { get; }
        Vec3 Rotation { get; }
        float FieldOfView { get; }
    }

}
