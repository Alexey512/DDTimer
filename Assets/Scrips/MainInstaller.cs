using System;
using Assets.Scrips.Common.Actions;
using Assets.Scrips.Common.InputSystem;
using Assets.Scrips.Common.Storage;
using Assets.Scrips.Common.Visual;
using Assets.Scrips.Game;
using Assets.Scrips.Game.Config;
using Assets.Scrips.Game.Timers;
using Assets.Scripts.Common.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scrips
{
    public class MainInstaller : MonoInstaller
	{
		[SerializeField]
		private WindowRoot _windowRoot;

		[SerializeField]
		private GameObject _visualRoot;

		[SerializeField]
		private TextAsset _gameConfig;

        [SerializeField]
        private VisualFactory _visualFactory;
        
        [SerializeField]
        private PrefabsFactory _prefabsFactory;
        
        [SerializeField]
        private WindowFactory _windowFactory;

		public override void InstallBindings()
		{
            Container.Bind<IInputManager>().To<InputManager>().FromNewComponentOnRoot().AsSingle();

            Container.Bind<IDataStorage>().To<DataStorage>().AsSingle();
			Container.Bind<IGameConfig>().FromInstance(new GameConfig(_gameConfig));
			Container.Bind(typeof(ITimersController), typeof(IDisposable), typeof(ITickable)).To<TimersController>().AsSingle();

			Container.Bind(typeof(IActionsSequencer), typeof(ITickable)).To<ActionsSequencer>().AsSingle();

            Container.Bind<IUIManager>().To<UIManager>().AsSingle();
            Container.Bind<IWindowRoot>().To<WindowRoot>().FromInstance(_windowRoot);
            Container.Bind<VisualRoot>().FromNewComponentOn(_visualRoot).AsSingle();

            Container.Bind<IVisualFactory>().To<VisualFactory>().FromInstance(_visualFactory).AsSingle();
            Container.Bind<IPrefabsFactory>().To<PrefabsFactory>().FromInstance(_prefabsFactory).AsSingle();
            Container.Bind<IWindowFactory>().To<WindowFactory>().FromInstance(_windowFactory).AsSingle();
	        Container.QueueForInject(_visualFactory);
            Container.QueueForInject(_prefabsFactory);
            Container.QueueForInject(_windowFactory);

            Container.Bind(typeof(GameScene), typeof(IInitializable), typeof(IDisposable)).To<GameScene>().FromComponentsInHierarchy().AsTransient();
        }
	}
}
