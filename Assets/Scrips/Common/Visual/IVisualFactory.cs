using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips.Common.Visual
{
	public interface IVisualFactory
	{
		Sprite GetIcon(string id);
		
		Sprite GetSprite(string id);
	}
}
