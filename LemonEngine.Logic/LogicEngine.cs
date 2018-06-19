using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Logic.Scene;
using LemonEngine.Infrastructure.Render.Renderable;
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

        private void Iterate()
        {

            _context.Iterate();
            SyncWithRenderEngine();
            AfterUpdateEventHandler?.Invoke(this, EventArgs.Empty);
        }

        private void SyncWithRenderEngine()
        {
            _context.SyncObjects(_renderService);            
        }
    }
}
