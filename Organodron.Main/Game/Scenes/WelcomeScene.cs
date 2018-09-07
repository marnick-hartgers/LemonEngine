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
            ent.Rotation.Y = (counter / 100f) % FMath.PI2;
        }

        public override void Load(IGameContext context)
        {
            base.Load(context);
            context.GraphicsContext.SetClearColor(new Vec3(0.1f,0.1f,0.1f));

            //context.AddMaintainable(new FirstPersonCameraMaintainable());
            context.CameraContext.Position.Y = 3;
            
            ent = new TestCube();
            ent.Position.X = 0;
            ent.Position.Z = -20;
            context.AddEntity(ent);
            
        }

        public override void Unload(IGameContext context)
        {
            base.Unload(context);
        }
    }
}
