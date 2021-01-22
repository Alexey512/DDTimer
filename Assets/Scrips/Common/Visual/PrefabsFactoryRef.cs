using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.Storage;
using UnityEngine;
using Zenject;

namespace Assets.Scrips.Common.Visual
{
	public class PrefabsFactoryRef: AddressableScriptableObject<PrefabsFactory>, IPrefabsFactory
	{
		public PrefabsFactoryRef(DiContainer container) : base(container)
		{
		}

		public GameObject Create(string id, Transform parent = null)
		{
			return Owner?.Create(id, parent);
		}

        public T Create<T>(string id, Transform parent = null) where T : MonoBehaviour
        {
            return Owner?.Create<T>(id, parent);
        }

        public void Remove(GameObject obj, bool moveInCache = false)
		{
			Owner.Remove(obj);
		}
	}
}
