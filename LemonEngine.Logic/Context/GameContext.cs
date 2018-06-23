using System;
using System.Collections.Generic;
using LemonEngine.Infrastructure.Logic.Maintainable;
using LemonEngine.Infrastructure.Logic.Objects;
using LemonEngine.Infrastructure.Logic.Output;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Types;
using LemonEngine.Logic.Context;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public class GameContext : IGameContext
    {
        private IScene _currentScene = null;
        public IScene CurrentLevel => _currentScene;
        private GraphicsContext _graphicsContext = new GraphicsContext();
        public IGraphicsContext GraphicsContext => _graphicsContext;

        private CameraContext _cameraContext = new CameraContext();
        public ICameraContext CameraContext => _cameraContext;

        private Vec2 _mouseMovement = new Vec2();
        public Vec2 MouseMovement => _mouseMovement;

        private List<IEntity> _entities = new List<IEntity>();
        private List<IMaintainable> _maintainables = new List<IMaintainable>();

        public void Iterate()
        {
            if (_currentScene != null)
            {
                _currentScene.Iterate(this);
            }
            foreach (IMaintainable m in _maintainables)
            {
                m.Update(this);
            }
        }

        public void LoadLevel(IScene scene)
        {
            _currentScene?.Unload(this);
            scene.Load(this);
            _currentScene = scene;
        }

        public void SetMouseMovement(Vec2 movement)
        {
            _mouseMovement = movement;
        }

        public void AddEntity(IEntity entity)
        {
            _entities.Add(entity);
            if (entity is IMaintainable)
            {
                _maintainables.Add((IMaintainable)entity);
            }
        }

        public void AddMaintainable(IMaintainable maintainable)
        {
            _maintainables.Add(maintainable);
        }

        public void SyncObjects(IRenderService renderService)
        {
            _graphicsContext.Sync(renderService);
            _cameraContext.Sync(renderService);
            SyncEnities(renderService);
        }

        private void SyncEnities(IRenderService renderService)
        {
            LogicOutputContainer output = new LogicOutputContainer();
            foreach (IEntity e in _entities)
            {
                output.AddRendableDefenition(new RenderbleDefenition()
                {
                    Id = e.Id,
                    ModelName = e.ModelName,
                    Position = e.Position,
                    PositionDelta = e.PositionDelta,
                    Rotation = e.Rotation,
                    RotationDelta = e.RotationDelta,
                    Scale = e.Scale
                });
            }
            renderService.ReceiveOutput(output);

        }
    }
}
