using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scrips.Common.InputSystem;
using Assets.Scrips.Common.Utils;
using Assets.Scrips.Common.Visual;
using Assets.Scrips.Game.Config;
using Assets.Scrips.Game.UI;
using Assets.Scripts.Common.UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scrips.Game
{
    public class GameScene: MonoBehaviour, IInitializable, IDisposable
    {
        [Inject] 
        private IGameConfig _config;

        [Inject]
        private IPrefabsFactory _factory;

        [Inject]
        private IInputManager _inputManager;

        [Inject]
        private IUIManager _uiManager;

        public void Initialize()
        {
	        _uiManager.Open<MainMenu>();
        }
        
        public void Dispose()
        {
        }
    }
}
