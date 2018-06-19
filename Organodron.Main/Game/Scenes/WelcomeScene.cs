using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Logic.Entity;
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
        Random r = new Random();
        public override void Iterate(IGameContext context)
        {
            base.Iterate(context);
        }

        public override void Load(IGameContext context)
        {
            base.Load(context);
            context.GraphicsContext.SetClearColor(new Vec3((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble()));

            Entity ent1 = new Entity("trainengine");
            ent1.Position.X = -4;
            ent1.Position.Y = 10;
            context.AddEntity(ent1);
            Entity ent2 = new Entity("building-office-small");
            ent2.Position.X = 4;
            context.AddEntity(ent2);
        }

        public override void Unload(IGameContext context)
        {
            base.Unload(context);
        }
    }
}
