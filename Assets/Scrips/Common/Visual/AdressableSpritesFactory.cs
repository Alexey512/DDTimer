using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneLine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scrips.Common.Visual
{
	[CreateAssetMenu(fileName = "AdressableSpritesAsset", menuName = "Game/AdressableSpritesAsset")]
	public class AdressableSpritesFactory: ScriptableObject, IAdressableSpritesFactory
	{
		[Serializable]
		public class Item
		{
			public string Id;

			public AssetReferenceSprite Resource;
		}

		[SerializeField, OneLine]
		public List<Item> _items = new List<Item>();

		private readonly Dictionary<AssetReference, AsyncOperationHandle<Sprite>> _asyncOperationHandles = new Dictionary<AssetReference, AsyncOperationHandle<Sprite>>();

		private readonly Dictionary<AssetReference, List<Action<Sprite>>> _asyncOperationCallbacks = new Dictionary<AssetReference, List<Action<Sprite>>>();

		public void LoadResource(string id, Action<Sprite> callback)
		{
			var item = _items.FirstOrDefault(i => i.Id == id);
			if (item?.Resource == null)
			{
				callback?.Invoke(null);
				return;
			}

			if (_asyncOperationHandles.ContainsKey(item.Resource))
			{
				if (_asyncOperationHandles[item.Resource].IsDone)
				{
					callback?.Invoke(_asyncOperationHandles[item.Resource].Result);
				}
				else
				{
					AddCallback(item.Resource, callback);
				}

				return;
			}

			var op = item.Resource.LoadAssetAsync();
			_asyncOperationHandles[item.Resource] = op;
			AddCallback(item.Resource, callback);

			op.Completed += (operation) =>
			{
				if (_asyncOperationCallbacks.TryGetValue(item.Resource, out var callbacks))
				{
					foreach (var asyncCallback in callbacks)
					{
						asyncCallback?.Invoke(op.Result);
					}
					callbacks.Clear();
				}
			};
		}

		public void ReleaseResource(string id, Action<Sprite> callback)
		{
			RemoveCallback(callback);
		}

		private void AddCallback(AssetReference resource, Action<Sprite> callback)
		{
			if (!_asyncOperationCallbacks.ContainsKey(resource))
			{
				_asyncOperationCallbacks[resource] = new List<Action<Sprite>>();
			}
			_asyncOperationCallbacks[resource].Add(callback);
		}

		private void RemoveCallback(Action<Sprite> callback)
		{
			foreach (var callbacks in _asyncOperationCallbacks)
			{
				if (callbacks.Value.Contains(callback))
				{
					callbacks.Value.Remove(callback);
					break;
				}
			}
		}
	}
}
