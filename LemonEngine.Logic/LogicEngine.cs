using LemonEngine.Infrastructure.Logic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Logic
{
    public class LogicEngine
    {
        private GameContext _context = new GameContext();

        public void Iterate()
        {
            _context.Iterate();
        }
    }
}
