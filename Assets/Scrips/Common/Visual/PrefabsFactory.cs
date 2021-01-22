using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using OneLine;
using Zenject;

namespace Assets.Scrips.Common.Visual
{
	[CreateAssetMenu(fileName = "CustomPrefabsAsset", menuName = "Game/CustomPrefabsAsset", order = 1)]
	public class PrefabsFactory: ScriptableObject, IPrefabsFactory
	{
		[Serializable]
		public class PrefabItem
		{
			public string Id;

			public GameObject Prefab;
		}

		[SerializeField, OneLine, HideLabel]
		private List<PrefabItem> _Prefabs;

		private readonly Dictionary<string, GameObject> _cache = new Dictionary<string, GameObject>();

        [Inject]
		private DiContainer _container; 

		public GameObject Create(string id, Transform parent = null)
		{
			if (_cache.TryGetValue(id, out GameObject obj))
			{
				obj.SetActive(true);
				obj.transform.parent = parent;
				return obj;
			}

			var item = _Prefabs.Find(e => e.Id == id);
			if (item == null)
				return null;

			GameObject instance = _container.InstantiatePrefab(item.Prefab, parent);
			return instance;
		}

        public T Create<T>(string id, Transform parent = null) where T : MonoBehaviour
        {
            return Create(id, parent)?.GetComponent<T>();
        }

        public void Remove(GameObject obj, bool moveInCache = false)
		{
			if (obj == null)
				return;

			if (moveInCache)
			{
				obj.transform.parent = null;
				obj.SetActive(false);
			}
			else
			{
				GameObject.Destroy(obj);
			}
		}
	}
}
