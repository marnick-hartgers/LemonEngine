using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Logic.Entity;
using LemonEngine.Logic.Maintainables.Camera;
using LemonEngine.Logic.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolveNewWorld.Scenes
{
    public class SpashScreen: GameScene
    {
        public SpashScreen() : base()
        {

        }

        Random r = new Random();

        public override void Iterate(IGameContext context)
        {
            base.Iterate(context);
        }

        public override void Load(IGameContext context)
        {
            base.Load(context);
            context.GraphicsContext.SetClearColor(new Vec3(0.1f, 0.1f, 0.1f));

            //context.AddMaintainable(new FirstPersonCameraMaintainable());
            context.CameraContext.Position.X = 1;
            context.CameraContext.Position.Y = 4;
            context.CameraContext.Rotation.X = 0.1f;
            context.CameraContext.Rotation.Y = 0.1f;
            //context.AddMaintainable(new FirstPersonCameraMaintainable());

            addGrassPlanes(context);

            Entity tree1 = new Entity("Large_Oak_Green");
            tree1.Position.X = 0;
            tree1.Position.Z = -15;
            tree1.Position.Y = 0;
            context.AddEntity(tree1);

        }

        public override void Unload(IGameContext context)
        {
            base.Unload(context);
        }

        private void addGrassPlanes(IGameContext context)
        {
            for (int x=0;x< 10;x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Entity grass = new Entity("Plate_Grass_01");
                    grass.Position.X = -15f + 3f * x;
                    grass.Position.Z = -25f + 3f * y;
                    grass.Position.Y = 0;
                    //test.RotationalVelocity.X = 0.003f;
                    //test.RotationalVelocity.Y = 0.008f;
                    //test.RotationalVelocity.Z = 0.04f;
                    context.AddEntity(grass);
                }
            }
        }
    }
}
