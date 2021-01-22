using System;
using System.Collections.Generic;
using System.Linq;
using OneLine;
using UnityEngine;

namespace Assets.Scrips.Common.Visual
{

	[CreateAssetMenu(fileName = "VisualPrefabsAsset", menuName = "Game/VisualPrefabsAsset", order = 1)]
	public class VisualFactory: ScriptableObject, IVisualFactory
	{
		[Serializable]
		public class PrefabItem
		{
			public string Id;

			public Sprite Resource;
		}

		[SerializeField, OneLine, HideLabel]
		private List<PrefabItem> _sprites;

		public Sprite GetIcon(string id)
		{
			return GetSprite($"{id}_icon");
		}

		public Sprite GetSprite(string id)
		{
			var item = _sprites.FirstOrDefault(s => s.Id == id);
			return item?.Resource;
		}

		
	}
}
