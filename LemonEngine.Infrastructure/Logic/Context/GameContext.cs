using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LemonEngine.Infrastructure.Logic.Scene;

namespace LemonEngine.Infrastructure.Logic.Context
{
    public class GameContext
    {
        private List<IScene> _scenes = new List<IScene>();
        public List<IScene> Levels => _scenes;
        private IScene _currentScene = null;
        public IScene CurrentLevel => _currentScene;


        public void Iterate()
        {
            if (_currentScene != null)
            {
                _currentScene.Iterate(this);
            }
        }

        public void LoadLevel(IScene scene)
        {
            _currentScene.Unload(this);
            scene.Load(this);
            _currentScene = scene;
        }
    }
}
