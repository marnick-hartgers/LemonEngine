using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Logic.Scene
{
    public abstract class GameScene : IScene
    {
        public virtual void Iterate(IGameContext context)
        {
            
        }

        public virtual void Load(IGameContext context)
        {
            
        }

        public virtual void Unload(IGameContext context)
        {
            
        }
    }
}
