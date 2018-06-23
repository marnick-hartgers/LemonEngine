using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Math
{
    public class FMath
    {
        private static float _pI = (float) System.Math.PI;
        private static float _pI2 = (float)System.Math.PI * 2;

        public static float PI => _pI;
        public static float PI2 => _pI2;

        public static float Floor(float input)
        {
            return input - input % 1;
        }

        public static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public static float Min(float a, float b)
        {
            return a < b ? a : b;
        }

        public static float Sin(float input)
        {
            return (float)System.Math.Sin(input);
        }
        public static float Cos(float input)
        {
            return (float)System.Math.Cos(input);
        }
    }
}
