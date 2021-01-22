using Assets.Scrips.Common.Storage;
using UnityEngine;
using Zenject;

namespace Assets.Scrips.Common.Visual
{
	public class VisualFactoryRef: AddressableScriptableObject<VisualFactory>, IVisualFactory
	{
		public VisualFactoryRef(DiContainer container): base(container)
		{
		}

		public Sprite GetIcon(string id)
		{
			return Owner?.GetIcon(id);
		}

		public Sprite GetSprite(string id)
		{
			return Owner?.GetSprite(id);
		}
	}
}
