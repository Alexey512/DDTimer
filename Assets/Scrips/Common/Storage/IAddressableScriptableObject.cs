using System;
using System.Collections;
using UnityEngine.AddressableAssets;

namespace Assets.Scrips.Common.Storage
{
	public interface IAddressableScriptableObject
	{
		IEnumerator Initialize(AssetReference assetRef, Action<float> progressCallback = null,
			Action completeCallback = null);
	}
}
