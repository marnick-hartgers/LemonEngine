using EvolveNewWorld.Scenes;
using LemonEngine.Logic;
using LemonEngine.RenderLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EvolveNewWorld
{
    public partial class EnvolveGameWindow : GameWindow.GameWindow
    {
        private LogicEngine _logicEngine;
        public EnvolveGameWindow() : base("Envolve New World")
        {
            InitializeComponent();
        }

        protected override void OnInit()
        {
            base.OnInit();
            _logicEngine = new LogicEngine();
            _logicEngine.SetOutput(RenderService);
            _logicEngine.Start(new SpashScreen());
        }

        protected override void OnMouseMovement(int x, int y)
        {
            base.OnMouseMovement(x, y);
            _logicEngine.ReceiveMouseMovement(x, y);
        }
    }
}
