using LemonEngine.Infrastructure.Math;
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
        public Vec3(Vec3 vec3)
        {
            _values = new[] { vec3.X, vec3.Y, vec3.Z };
        }

        public Vec3 GetNormal()
        {
            float distance = (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vec3(X / distance, Y / distance, Z / distance);
        }

        public float Max
        {
            get {
                return FMath.Max(FMath.Max(_values[0], _values[1]), _values[1]);
            }
        }
        public float Min
        {
            get
            {
                return FMath.Min(FMath.Min(_values[0], _values[1]), _values[1]);
            }
        }

        public void CopyFrom(Vec3 input)
        {
            this._values[0] = input.X;
            this._values[1] = input.Y;
            this._values[2] = input.Z;
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

        public static void Copy(Vec3 original, Vec3 copy)
        {
            //copy = new Vec3(original.X, original.Y, original.Z);
            copy.X = original.X;
            copy.Y = original.Y;
            copy.Z = original.Z;
        }

        public float[] AsArray => _values;
    }
}
