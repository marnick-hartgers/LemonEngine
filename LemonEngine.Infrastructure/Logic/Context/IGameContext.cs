using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Logic.Maintainable;
using LemonEngine.Infrastructure.Logic.Objects;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public interface IGameContext
    {
        void Iterate();

        void LoadLevel(IScene scene);

        void AddEntity(IEntity entity);

        void AddMaintainable(IMaintainable maintainable);

        void SyncObjects(IRenderService engine);        

        IGraphicsContext GraphicsContext { get; }

        ICameraContext CameraContext { get; }

        Vec2 MouseMovement { get; }
    }
}
