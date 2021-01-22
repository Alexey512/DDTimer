using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Storage
{
	public class FakeStorage: IDataStorage
	{
		public void Load(Action complete = null)
		{
		}

		public bool HasChanges => false;

		public float GetFloat(string key, float defaultValue = 0)
		{
			return 0;
		}

		public void SetFloat(string key, float value)
		{
		}

		public int GetInt(string key, int defaultValue = 0)
		{
			return 0;
		}

		public void SetInt(string key, int value)
		{
		}

		public void SetBool(string key, bool value)
		{
		}

		public bool GetBool(string key, bool defaultValue = false)
		{
			return false;
		}

		public long GetLong(string key, long defaultValue = 0)
		{
			return 0;
		}

		public void SetLong(string key, long value)
		{
		}

		public string GetString(string key, string defaultValue = "")
		{
			return string.Empty;
		}

		public void SetString(string key, string value)
		{
		}

		public T GetEnum<T>(string key, T defaultValue = default(T)) where T : struct, IConvertible
		{
			return default(T);
		}

		public void SetEnum<T>(string key, T value) where T : struct, IConvertible
		{
		}

		public void RemoveKey(string key)
		{
		}

		public bool HasKey(string key)
		{
			return false;
		}

		public void Clear()
		{
		}

		public void Save()
		{
		}
	}
}
