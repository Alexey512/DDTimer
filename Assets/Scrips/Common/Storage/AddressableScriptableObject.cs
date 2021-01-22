using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Assets.Scrips.Common.Storage
{
	public class AddressableScriptableObject<T>: IAddressableScriptableObject where T: ScriptableObject
	{
		protected T Owner;

		private readonly DiContainer _container;

		private float _progress;

		public AddressableScriptableObject(DiContainer container)
		{
			_container = container;
		}

		public IEnumerator Initialize(AssetReference assetRef, Action<float> progressCallback = null, Action completeCallback = null)
		{
			_progress = 0f;
			
			var handle = assetRef.LoadAssetAsync<T>();
			yield return null;
 
			while (!handle.IsDone)
			{
				_progress = handle.PercentComplete;
				progressCallback?.Invoke(handle.PercentComplete);
				
				yield return null;
			}

			if (handle.Status == AsyncOperationStatus.Succeeded)
			{
				_progress = 1f;
				_container.Inject(handle.Result);
				Owner = handle.Result;
			}

			completeCallback?.Invoke();
		}

		public float Progress => _progress;
	}
}
