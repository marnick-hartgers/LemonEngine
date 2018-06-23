using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Math;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Logic.Entity;
using LemonEngine.Logic.Maintainables.Camera;
using LemonEngine.Logic.Scene;
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
        public override void Iterate(IGameContext context)
        {
            base.Iterate(context);
            counter++;
            context.CameraContext.Position.X = 40 + 20 * FMath.Sin(counter / 300f);
            context.CameraContext.Position.Z = 40 + 20 * FMath.Cos(counter / 300f);
        }

        public override void Load(IGameContext context)
        {
            base.Load(context);
            context.GraphicsContext.SetClearColor(new Vec3(0.6f,0.6f,1f));

            context.AddMaintainable(new FirstPersonCameraMaintainable());
            context.CameraContext.Position.Y = 10;
            
            Entity ent = new Entity("world");
            ent.Position.X = 80;
            ent.Position.Z = 0;
            context.AddEntity(ent);
            
        }

        public override void Unload(IGameContext context)
        {
            base.Unload(context);
        }
    }
}
