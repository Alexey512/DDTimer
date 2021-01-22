using UnityEngine;

namespace Assets.Scripts.Common.UI
{
	public class WindowRoot: MonoBehaviour, IWindowRoot
	{
		public Transform Root => this.transform;
	}
}
