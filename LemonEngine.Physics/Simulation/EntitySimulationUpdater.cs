using LemonEngine.Infrastructure.Logic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Physics.Simulation
{
    public class EntitySimulationUpdater
    {
        public void Run(ref List<IEntity> entities, float timeScale)
        {
            foreach (IEntity ent in entities)
            {
                ApplyVelocity(ent, timeScale);
            }
        }

        private void ApplyVelocity(IEntity entity, float timeScale)
        {
            entity.Position.CopyFrom(entity.Position + (entity.Velocity * timeScale));
            entity.Rotation.CopyFrom(entity.Rotation + (entity.RotationalVelocity * timeScale));
        }

        

    }
}
