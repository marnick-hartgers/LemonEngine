using System;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.RenderLogic.Events
{
    public class ResizedEventArgs : EventArgs
    {
        public Vec2 NewResolution { get; }

        public ResizedEventArgs(int x, int y)
        {
            NewResolution = new Vec2(x,y);
        }
    }
}