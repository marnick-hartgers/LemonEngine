using System;
using LemonEngine.Infrastructure.Logic.Context;
using LemonEngine.Infrastructure.Render.Camera;
using LemonEngine.Infrastructure.Render.Renderable;
using LemonEngine.Infrastructure.Types;

namespace LemonEngine.Logic.Context
{
    public class CameraContext : ICameraContext
    {
        public CameraContext()
        {
            Position = new Vec3();
            Rotation = new Vec3();
            FieldOfView = 45f;
        }

        public Vec3 Position { get; }

        public Vec3 Rotation { get; }

        public float FieldOfView { get; }

        public void Sync(IRenderService renderService)
        {
            renderService.SetCamera(new CameraSettings {
                Position = new Vec3(Position),
                Rotation = new Vec3(Rotation),
                FieldOfView = FieldOfView
            });
        }
    }
}
