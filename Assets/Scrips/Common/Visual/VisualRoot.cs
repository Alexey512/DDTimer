using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scrips.Common.Visual
{
	public class VisualRoot: MonoBehaviour
	{
		public Transform Root => this.transform;

		public List<T> FindComponents<T>() where T : class
		{
			List<T> result = new List<T>(GetComponentsInChildren<T>());
			FindComponents<T>(Root, result);
			return result;
		}

		private void FindComponents<T>(Transform owner, List<T> result) where T : class
		{
			result.AddRange(owner.GetComponentsInChildren<T>());
			for (int i = 0; i < owner.childCount; i++)
			{
				FindComponents(owner.GetChild(i).transform, result);
			}
		}
	}
}
