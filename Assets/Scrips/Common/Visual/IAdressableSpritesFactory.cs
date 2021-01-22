using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips.Common.Visual
{
	public interface IAdressableSpritesFactory
	{
		void LoadResource(string id, Action<Sprite> callback);

		void ReleaseResource(string id, Action<Sprite> callback);

	}
}
