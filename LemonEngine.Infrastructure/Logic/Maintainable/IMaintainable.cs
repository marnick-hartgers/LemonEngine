using LemonEngine.Infrastructure.Logic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Logic.Maintainable
{
    public interface IMaintainable
    {
        void Update(IGameContext gameContext);
    }
}
