using LemonEngine.Logic;
using Organodron.Main.Game.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organodron.Main.Window
{
    class OrganodronMainWindow : GameWindow.GameWindow
    {
        private LogicEngine _logicEngine;
        public OrganodronMainWindow() : base("Organodron")
        {
            
        }

        protected override void OnInit()
        {
            base.OnInit();
            _logicEngine = new LogicEngine();
            _logicEngine.SetOutput(RenderService);
            _logicEngine.Start(new WelcomeScene());
        }
    }
}
