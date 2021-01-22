using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrips.Common.Storage
{

	public interface IDataStorage
	{
		void Load(Action complete = null);
		
		bool HasChanges { get; }

		float GetFloat(string key, float defaultValue = 0);
		void SetFloat(string key, float value);
		
		int GetInt(string key, int defaultValue = 0);
		void SetInt(string key, int value);

		void SetBool(string key, bool value);

		bool GetBool(string key, bool defaultValue = false);

		long GetLong(string key, long defaultValue = 0);
		void SetLong(string key, long value);

		string GetString(string key, string defaultValue = "");
		void SetString(string key, string value);

		T GetEnum<T>(string key, T defaultValue = default(T)) where T : struct, IConvertible;
		void SetEnum<T>(string key, T value) where T : struct, IConvertible;

		void RemoveKey(string key);

		bool HasKey(string key);

		void Clear();

		void Save();
	}

}