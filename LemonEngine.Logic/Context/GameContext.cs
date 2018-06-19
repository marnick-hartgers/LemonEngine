using System;
using System.Collections.Generic;
using LemonEngine.Infrastructure.Logic.Objects;
using LemonEngine.Infrastructure.Logic.Output;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Logic.Context;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public class GameContext : IGameContext
    {
        private IScene _currentScene = null;
        public IScene CurrentLevel => _currentScene;
        private IGraphicsContext _graphicsContext = new GraphicsContext();
        public IGraphicsContext GraphicsContext => _graphicsContext;

        private List<IEntity> _entities = new List<IEntity>();

        public void Iterate()
        {
            if (_currentScene != null)
            {
                _currentScene.Iterate(this);
            }
            foreach (IEntity e in _entities)
            {
                e.Update();
            }
        }

        public void LoadLevel(IScene scene)
        {
            _currentScene?.Unload(this);
            scene.Load(this);
            _currentScene = scene;
        }

        public void AddEntity(IEntity entity)
        {
            _entities.Add(entity);
        }

        public void SyncObjects(IRenderService renderService)
        {
            _graphicsContext.Sync(renderService);
            SyncEnities(renderService);
        }

        private void SyncEnities(IRenderService renderService)
        {
            LogicOutputContainer output = new LogicOutputContainer();
            foreach (IEntity e in _entities)
            {
                output.AddRendableDefenition(new RenderbleDefenition() {
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
