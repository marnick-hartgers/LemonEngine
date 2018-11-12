using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Math;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Logic.Entity;
using LemonEngine.Logic.Maintainables.Camera;
using LemonEngine.Logic.Scene;
using Organodron.Main.Game.Objects.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organodron.Main.Game.Scenes
{
    public class WelcomeScene : GameScene
    {
        long counter = 0;
        Random r = new Random();
        Entity ent;
        public override void Iterate(IGameContext context)
        {
            base.Iterate(context);
            counter++;
            //context.CameraContext.Position.X = 10 * FMath.Sin(counter / 300f);
            //context.CameraContext.Position.Z = 10 * FMath.Cos(counter / 300f);
            //ent.Rotation.Y = (counter / 100f) % FMath.PI2;
        }

        public override void Load(IGameContext context)
        {
            base.Load(context);
            context.GraphicsContext.SetClearColor(new Vec3(0.1f,0.1f,0.1f));

            //context.AddMaintainable(new FirstPersonCameraMaintainable());
            context.CameraContext.Position.Y = 3;
            
            ent = new TestCube();
            ent.Position.X = 0;
            ent.Position.Z = -15;
            ent.Position.Y = 3;
            ent.RotationalVelocity.X = 0.003f;
            ent.RotationalVelocity.Y = 0.008f;
            ent.RotationalVelocity.Z = 0.004f;
            context.AddEntity(ent);

            Entity ent2 = new TestCube();
            ent2.Position.X = 4;
            ent2.Position.Z = -10;
            ent2.Position.Y = 2;
            ent2.RotationalVelocity.X = -0.008f;
            ent2.RotationalVelocity.Y = 0.002f;
            ent2.RotationalVelocity.Z = -0.008f;
            context.AddEntity(ent2);

            Entity ent3 = new TestCube();
            ent3.Position.X = -3;
            ent3.Position.Z = -10;
            ent3.Position.Y = 5;
            ent3.RotationalVelocity.X = -0.001f;
            ent3.RotationalVelocity.Y = 0.008f;
            ent3.RotationalVelocity.Z = -0.001f;
            context.AddEntity(ent3);

            Entity ent4 = new TestCube();
            ent4.Position.X = -2;
            ent4.Position.Z = -10;
            ent4.Position.Y = 1;
            ent4.RotationalVelocity.X = 0.008f;
            ent4.RotationalVelocity.Y = 0.008f;
            ent4.RotationalVelocity.Z = 0.001f;
            context.AddEntity(ent4);

        }

        public override void Unload(IGameContext context)
        {
            base.Unload(context);
        }
    }
}
