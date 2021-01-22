using System;
using System.Collections.Generic;
using Assets.Scrips.Common.Storage;
using Assets.Scripts.Common.UI;
using Assets.Scripts.Common.UI.Controller;
using OneLine;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Common.UI
{
	[CreateAssetMenu(fileName = "WindowPrefabsAsset", menuName = "Game/WindowPrefabsAsset", order = 1)]
	public class WindowFactory: ScriptableObject, IWindowFactory
	{
		[Serializable]
		public class PrefabItem
		{
			public string Id;

			public WindowController Prefab;
		}

		[SerializeField, OneLine, HideLabel]
		private List<PrefabItem> _Prefabs;

		private readonly Dictionary<Type, PrefabItem> _cache = new Dictionary<Type, PrefabItem>();

		private IWindowRoot _root;

		private DiContainer _container; 

		[Inject]
        public void Construct(DiContainer container, IWindowRoot root)
        {
            _container = container;
			_root = root;
        }

		public T Create<T>() where T : class, IWindowController
		{
			PrefabItem item;
			Type windowType = typeof(T);
			if (!_cache.TryGetValue(windowType, out item))
			{
				item = _Prefabs.Find(e => e.Prefab is T);
				if (item == null)
					return null;
				_cache[windowType] = item;
			}

			GameObject instance = _container.InstantiatePrefab(item.Prefab.Owner.gameObject, _root.Root);
			return instance.GetComponent<IWindowController>() as T;
		}

		public void Remove(IWindowController window)
		{
			GameObject.Destroy(window.Owner.gameObject);
		}
	}
}
