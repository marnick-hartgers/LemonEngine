using System;

namespace LemonEngine.Infrastructure.Types
{
    public class Vec3
    {
        private float[] _values;

        public Vec3()
        {
            _values = new[] { 0.0f, 0.0f, 0.0f };
        }
        public Vec3(float x, float y, float z)
        {
            _values = new[] { x, y, z };
        }

        public Vec3 GetNormal()
        {
            float distance = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vec3(X / distance, Y / distance, Z / distance);
        }


        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X - b.X,
                            a.Y - b.Y,
                            a.Z - b.Z);
        }
        public static Vec3 operator *(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X * b.X,
                            a.Y * b.Y,
                            a.Z * b.Z);
        }
        public static Vec3 operator /(Vec3 a, Vec3 b)
        {
            return new Vec3(a.X / b.X,
                            a.Y / b.Y,
                            a.Z / b.Z);
        }

        public float X
        {
            get
            {
                return _values[0];
            }
            set
            {
                _values[0] = value;
            }
        }
        public float Y
        {
            get
            {
                return _values[1];
            }
            set
            {
                _values[1] = value;
            }
        }
        public float Z
        {
            get
            {
                return _values[2];
            }
            set
            {
                _values[2] = value;
            }
        }

        public float[] AsArray => _values;
    }
}
