using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Types;
using LemonEngine.RenderLogic;
using System;
using System.Threading;

namespace LemonEngine.Logic
{
    public class LogicEngine
    {
        private GameContext _context;
        private Timer _iterateTimer;
        private long _lastRun = 0;

        private Vec2 _mouseMovement = new Vec2();
        public EventHandler AfterUpdateEventHandler;
        private IRenderService _renderService;

        public LogicEngine()
        {
            _iterateTimer = new Timer((sender) => Iterate());
            _context = new GameContext();
        }

        public void SetOutput(RenderService renderService)
        {
            _renderService = renderService;
        }

        public void Start(IScene beginScene)
        {
            _iterateTimer.Change(0,1000/60);
            _context.LoadLevel(beginScene);
        }

        public void Stop()
        {
            _iterateTimer.Change(Int32.MaxValue, Int32.MaxValue);
        }

        public void ReceiveMouseMovement(int x, int y)
        {
            _mouseMovement.X += x;
            _mouseMovement.Y += y;
        }

        private void Iterate()
        {
            _context.SetMouseMovement(new Vec2(_mouseMovement));
            _context.Iterate();
            SyncWithRenderEngine();
            AfterUpdateEventHandler?.Invoke(this, EventArgs.Empty);
            _mouseMovement.X = 0;
            _mouseMovement.Y = 0;
        }

        private void SyncWithRenderEngine()
        {
            _context.SyncObjects(_renderService);            
        }
    }
}
