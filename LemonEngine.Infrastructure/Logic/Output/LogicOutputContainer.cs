using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonEngine.Infrastructure.Logic.Output
{
    public class LogicOutputContainer
    {
        private List<RenderbleDefenition>rendables = new List<RenderbleDefenition>();

        public void AddRendableDefenition(RenderbleDefenition renderbleDefenition)
        {
            rendables.Add(renderbleDefenition);
        }

        public IReadOnlyList<RenderbleDefenition> GetRenderbleDefenitions() => rendables;
    }
}
