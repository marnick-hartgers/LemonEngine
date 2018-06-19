using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Logic.Objects;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Render.Renderable;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public interface IGameContext
    {
        void Iterate();

        void LoadLevel(IScene scene);

        void AddEntity(IEntity entity);

        void SyncObjects(IRenderService engine);        

        IGraphicsContext GraphicsContext { get; }
    }
}
