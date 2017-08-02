using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Types
{
    public class Int4
    {
        public Int4()
        {

        }
        public Int4(int one, int two, int three, int four)
        {
            One = one;
            Two = two;
            Three = three;
            Four = four;
        }

        public int One { get; set; }

        public int Two { get; set; }

        public int Three { get; set; }

        public int Four { get; set; }
    }
}
