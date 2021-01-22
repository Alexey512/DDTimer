using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips.Common.Visual
{
	public interface IAdressableFactory
	{
		void LoadResource(string id, Action<GameObject> callback);

		void ReleaseResource(GameObject resource);
	}
}
