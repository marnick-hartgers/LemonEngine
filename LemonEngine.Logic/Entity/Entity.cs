using LemonEngine.Infrastructure.Logic.Entity;
using LemonEngine.Infrastructure.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Logic.Entity
{
    public class Entity : IEntity
    {
        public Entity(string modelName)
        {
            Id = Guid.NewGuid();
            ModelName = modelName;
            Position = new Vec3();
            PositionDelta = new Vec3();
            Rotation = new Vec3();
            RotationDelta = new Vec3();
            Scale = new Vec3(1f,1f,1f);
        }

        public Guid Id { get; }

        public string ModelName { get; }

        public Vec3 Position { get; private set; }

        public Vec3 PositionDelta { get; private set; }

        public Vec3 Rotation { get; private set; }

        public Vec3 RotationDelta { get; private set; }

        public Vec3 Scale { get; private set; }

        public Vec3 Velocity { get; private set; }

        public Vec3 RotationalVelocity { get; private set; }



        public void Dispose()
        {

        }
    }
}
