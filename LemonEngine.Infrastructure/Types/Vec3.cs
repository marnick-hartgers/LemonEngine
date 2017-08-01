using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Types
{
    public class Vec3
    {
        private float _x, _y, _z;

        public Vec3()
        {

        }
        public Vec3(float x,float y,float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public float X{
            get {
                return _x;
            } set{
                _x = value;
            }
        }
        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }
        public float Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }


    }
}
