using System;

namespace LemonEngine.Infrastructure.Types
{
    public class Vec2
    {
        private float[] _values;

        public Vec2()
        {
            _values = new[] { 0.0f, 0.0f};
        }
        public Vec2(float x, float y)
        {
            _values = new[] { x, y };
        }

        public Vec2(Vec2 input)
        {
            _values = new[] { input.X, input.Y };
        }


        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X + b.X,
                a.Y + b.Y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X - b.X,
                            a.Y - b.Y);
        }
        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X * b.X,
                            a.Y * b.Y);
        }
        public static Vec2 operator /(Vec2 a, Vec2 b)
        {
            return new Vec2(a.X / b.X,
                            a.Y / b.Y);
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

        public float[] AsArray => _values;
    }
}
