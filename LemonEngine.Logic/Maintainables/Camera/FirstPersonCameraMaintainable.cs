using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Maintainable;
using LemonEngine.Infrastructure.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Logic.Maintainables.Camera
{
    public class FirstPersonCameraMaintainable : IMaintainable
    {
        public void Update(IGameContext gameContext)
        {
            ICameraContext camera = gameContext.CameraContext;
            camera.Rotation.Y += ((float)gameContext.MouseMovement.X / 80f);
            camera.Rotation.X += ((float)gameContext.MouseMovement.Y / 80f);

            if (camera.Rotation.X > FMath.PI / 2f)
            {
                camera.Rotation.X = FMath.PI / 2f;
            }else if (camera.Rotation.X < FMath.PI / -2f)
            {
                camera.Rotation.X = FMath.PI / -2f;
            }

        }
    }
}
