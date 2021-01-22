using System;
using UnityEngine;

namespace Assets.Scrips.Common.Storage
{
	public class DataStorage: IDataStorage
	{
		public void Load(Action complete = null)
		{
			complete?.Invoke();
		}

		public bool HasChanges => false;

		public float GetFloat(string key, float defaultValue = 0)
		{
			return PlayerPrefs.GetFloat(key, defaultValue);
		}
		
		public void SetFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
		}
		
		public int GetInt(string key, int defaultValue = 0)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}
		
		public void SetInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
		}

		public void SetBool(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}

		public bool GetBool(string key, bool defaultValue = false)
		{
			if (!PlayerPrefs.HasKey(key))
				return defaultValue;
			return PlayerPrefs.GetInt(key) != 0;
		}

		public long GetLong(string key, long defaultValue = 0)
		{
			string strVal = GetString(key);
			if (string.IsNullOrEmpty(strVal) || !long.TryParse(strVal, out long val))
				return defaultValue;

			return val;
		}

		public void SetLong(string key, long value)
		{
			SetString(key, value.ToString());
		}

		public string GetString(string key, string defaultValue = "")
		{
			return PlayerPrefs.GetString(key, defaultValue);
		}
		
		public void SetString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
		}
		
		public T GetEnum<T>(string key, T defaultValue = default(T)) where T : struct, IConvertible
		{
			if (!typeof(T).IsEnum) 
			{
				throw new ArgumentException("T must be an enumerated type");
			}

			var strValue = PlayerPrefs.GetString(key);

			if (Enum.TryParse<T>(strValue, out T value))
			{
				return value;
			}

			return defaultValue;
		}

		public void SetEnum<T>(string key, T value) where T : struct, IConvertible
		{
			if (!typeof(T).IsEnum) 
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			PlayerPrefs.SetString(key, value.ToString());
		}

		public void RemoveKey(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}

		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public void Clear()
		{
			PlayerPrefs.DeleteAll();
		}

		public void Save()
		{
			PlayerPrefs.Save();
		}
	}
}
