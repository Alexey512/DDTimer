using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips.Common.Utils
{
	public static class CanvasExtensions
	{
		public static Vector2 WorldToCanvas(this Canvas canvas, Vector3 worldPosition, Camera camera = null)
		{
			if (camera == null)
			{
				camera = Camera.main;
			}
 
			var viewportPosition = camera.WorldToViewportPoint(worldPosition);
			var canvasRect = canvas.GetComponent<RectTransform>();
 
			return new Vector2((viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
				(viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
		}
	}
}
