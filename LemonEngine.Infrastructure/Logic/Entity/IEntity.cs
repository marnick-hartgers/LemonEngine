using LemonEngine.Infrastructure.Types;
using System;

namespace LemonEngine.Infrastructure.Logic.Entity
{
    public interface IEntity
    {
        void Dispose();
        Guid Id { get; }
        string ModelName { get; }
        Vec3 Position { get; }
        Vec3 PositionDelta { get; }
        Vec3 Rotation { get; }
        Vec3 RotationDelta { get; }
        Vec3 Scale { get; }
        Vec3 Velocity { get; }
        Vec3 RotationalVelocity { get; }
    }
}
