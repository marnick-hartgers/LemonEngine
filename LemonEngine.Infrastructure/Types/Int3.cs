using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Types
{
    public class Int3
    {
        private int _x, _y, _z;

        public Int3()
        {

        }
        public Int3(int x,int y,int z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public int X{
            get {
                return _x;
            } set{
                _x = value;
            }
        }
        public int Y
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
        public int Z
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
