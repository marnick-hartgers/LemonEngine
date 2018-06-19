using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Logic.Context;

namespace LemonEngine.Infrastructure.Logic.Scene
{
    public interface IScene
    {
        void Load(IGameContext context);
        void Unload(IGameContext context);
        void Iterate(IGameContext context);
    }
}
