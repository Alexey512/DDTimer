using System;
using System.Collections.Generic;
using System.Linq;
using OneLine;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scrips.Common.Visual
{
	[CreateAssetMenu(fileName = "AdressablesFactory", menuName = "Game/AdressablesFactory")]
	public class AdressableFactory: ScriptableObject, IAdressableFactory
	{

		[Serializable]
		public class Item
		{
			public string Id;

			public AssetReference Resource;
		}

		[SerializeField, OneLine]
		private List<Item> _items = new List<Item>();

		public void LoadResource(string id, Action<GameObject> callback)
		{
			var item = _items.FirstOrDefault(i => i.Id == id);
			if (item?.Resource == null)
			{
				callback?.Invoke(null);
				return;
			}

			item.Resource.InstantiateAsync().Completed += handle =>
			{
				if (handle.IsDone)
				{
					callback?.Invoke(handle.Result);
				}
				else
				{
					callback?.Invoke(null);
				}
			};
		}

		public void ReleaseResource(GameObject resource)
		{
			Addressables.ReleaseInstance(resource);
		}
	}
}
