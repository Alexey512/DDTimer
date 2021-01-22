using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scrips.Common.Utils
{
	public static class GameObjectExtensions
	{
		public static Vector2 GetWorldSize(this GameObject root)
        {
            var sr = root.GetComponent<SpriteRenderer>();
            return sr != null ? sr.GetWorldSize() : Vector2.zero; 
        }
        
		public static Vector2 GetPivot(this GameObject root)
        {
            var sr = root.GetComponent<SpriteRenderer>();
            return sr != null ? sr.GetPivot() : Vector2.zero; 
		}

		public static Rect GetWorldRect(this GameObject root)
		{
            var sr = root.GetComponent<SpriteRenderer>();
            return sr != null ? sr.GetWorldRect() : Rect.zero; 
		}

		public static Vector2 GetPivot(this SpriteRenderer sr)
        {
            if (sr.sprite == null || sr.sprite.rect.size == Vector2.zero)
                return Vector2.zero;

			return sr.sprite.rect.center / sr.sprite.rect.size;
		}

		public static Vector2 GetWorldSize(this SpriteRenderer sr)
        {
			if (sr.drawMode != SpriteDrawMode.Simple)
			{
				return sr.size;
			}

            if (sr.sprite == null)
                return Vector2.zero;

            Vector2 spriteSize = sr.sprite.rect.size;
            Vector2 localSpriteSize = spriteSize / sr.sprite.pixelsPerUnit;
            Vector3 worldSize = localSpriteSize;
            worldSize.x *= sr.transform.lossyScale.x;
            worldSize.y *= sr.transform.lossyScale.y;

            return worldSize;
        }

		public static Rect GetWorldRect(this SpriteRenderer sr)
		{
			var rect = new Rect();
			var size = sr.GetWorldSize();
			var pivot = sr.GetPivot();

			rect.size = size;
			rect.center = pivot * size;
			rect.position = new Vector2(sr.transform.position.x, sr.transform.position.y) - rect.center;

			return rect;
		}

		public static Rect GetWorldRectInChildren(this GameObject root)
		{
			Rect totalRect = new Rect();
			totalRect.xMin = float.MaxValue / 2;
			totalRect.yMin = float.MaxValue / 2;
			totalRect.xMax = float.MinValue / 2;
			totalRect.yMax = float.MinValue / 2;
			
			bool isFind = false;
			GetWorldRectRecursive(root, ref totalRect, ref isFind);
			return isFind ? totalRect : Rect.zero;
		}

		public static void GetWorldRectRecursive(GameObject root, ref Rect totalRect, ref bool isFind)
		{
			var sr = root.GetComponent<SpriteRenderer>();
			if (sr != null)
			{
				Rect rootRect = sr.GetWorldRect();
				totalRect.xMin = Mathf.Min(totalRect.xMin, rootRect.xMin);
				totalRect.xMax = Mathf.Max(totalRect.xMax, rootRect.xMax);
				totalRect.yMin = Mathf.Min(totalRect.yMin, rootRect.yMin);
				totalRect.yMax = Mathf.Max(totalRect.yMax, rootRect.yMax);
				isFind = true;
			}

			for (int i = 0; i < root.transform.childCount; i++)
			{
				var child = root.transform.GetChild(i);
				GetWorldRectRecursive(child.gameObject, ref totalRect, ref isFind);
			}
		}

        public static Vector2 GetSize(this GameObject root)
        {
            var sr = root.GetComponent<SpriteRenderer>();
            if (sr == null || sr.sprite == null)
                return Vector2.zero;

			var worldSize = sr.GetWorldSize();
            Vector3 screenSize = 0.5f * worldSize / Camera.main.orthographicSize;
            screenSize.y *= Camera.main.aspect;
            return new Vector3(screenSize.x * Camera.main.pixelWidth, screenSize.y * Camera.main.pixelHeight, 0) * 0.5f;
        }

		public static List<T> GetAllComponentsInChildren<T>(this GameObject root) where T: Component
		{
			List<T> result = new List<T>();
			GetComponentsInChildrenRecursive<T>(root.transform, result);
			return result;
		}

		private static void GetComponentsInChildrenRecursive<T>(Transform root, List<T> result) where T: Component
		{
			result.AddRange(root.GetComponents<T>());
			for (int i = 0; i < root.childCount; i++)
			{
				GetComponentsInChildrenRecursive<T>(root.GetChild(i), result);
			}
		}

		public static Transform FindRecursive(this Transform root, string name)
		{
			var child = root.Find(name);
			if (child != null)
				return child;
			for (int i = 0; i < root.childCount; i++)
			{
				child = root.GetChild(i).FindRecursive(name);
				if (child != null)
					return child;
			}
			return null;
		}

		public static void RemoveAllChildren(this Transform root)
		{
			foreach (Transform child in root) 
			{
				GameObject.Destroy(child.gameObject);
			}
		}
	}
}
