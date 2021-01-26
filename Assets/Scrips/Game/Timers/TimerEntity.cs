using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.Storage;
using UnityEngine;

namespace Assets.Scrips.Game.Timers
{
	public class TimerEntity
	{
		public event Action<TimerEntity> Complete;
		
		private readonly IDataStorage _storage;

		private float _leftTime = 0;

		private bool _isPaused = false;

		public int Index { get; private set; }

		public string Id => $"timer_{Index}";

		public int Duration { get; set; } = 90;

		public bool IsPaused
		{
			get => _isPaused;
			set
			{
				_isPaused = value;
				Save();
			}
		}

		private static string TIME_KEY = "_left_time";

		private static string PAUSE_KEY = "_pause";

		public TimerEntity(int index, IDataStorage storage)
		{
			Index = index;
			_storage = storage;
			if (_storage.HasKey(Id + TIME_KEY))
			{
				_leftTime = _storage.GetFloat(Id + TIME_KEY);
				IsPaused = _storage.GetBool(Id + PAUSE_KEY);
			}
			else
			{
				_leftTime = Duration;
				IsPaused = true;
				Save();
			}
		}

		public void AddTime(int value)
		{
			_leftTime += value;
			if (_leftTime < 0)
			{
				_leftTime = 0;
			}
			Save();
		}

		public bool IsComplete()
		{
			return GetLeftTime() == 0;
		}

		public void ResetTime()
		{
			_leftTime = Duration;
			Save();
		}

		public int GetLeftTime()
		{
			return (int)_leftTime;
		}

		public void Save()
		{
			_storage.SetFloat(Id + TIME_KEY, _leftTime);
			_storage.SetBool(Id + PAUSE_KEY, IsPaused);
			
			_storage.Save();
		}

		public void Update()
		{
			if (IsPaused)
				return;
			
			_leftTime -= Time.deltaTime;
			if (_leftTime < 0)
			{
				_leftTime = 0;
			}

			if (IsComplete())
			{
				IsPaused = true;
				Complete?.Invoke(this);
			}
		}
	}
}
