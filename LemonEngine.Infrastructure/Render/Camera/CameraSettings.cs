using LemonEngine.Infrastructure.Types;


namespace LemonEngine.Infrastructure.Render.Camera
{
    public class CameraSettings
    {
        public Vec3 Position { get; set; }

        public Vec3 Rotation { get; set; }

        public float FieldOfView { get; set; }

        public static CameraSettings Empty()
        {
            return new CameraSettings {
                Position = new Vec3(),
                Rotation = new Vec3(),
                FieldOfView = 60f
            };
        }
    }
}
