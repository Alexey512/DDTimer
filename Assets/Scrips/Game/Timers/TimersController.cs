using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.Storage;
using Assets.Scrips.Game.Config;
using Zenject;

namespace Assets.Scrips.Game.Timers
{
	public class TimersController: ITimersController, IDisposable, ITickable
	{
		private readonly IGameConfig _config;

		private readonly IDataStorage _storage;

		private readonly List<TimerEntity> _timers = new List<TimerEntity>();

		public int TimersCount => _timers.Count;

		public TimerEntity GetTimer(int index)
		{
			return _timers[index];
		}

		public TimersController(IGameConfig config, IDataStorage storage)
		{
			_config = config;
			_storage = storage;
			
			LoadTimers();
		}

		private void LoadTimers()
		{
			for (int i = 0; i < _config.TimersCount; i++)
			{
				var timer = new TimerEntity(i, _storage);
				_timers.Add(timer);
			}
		}

		public void Dispose()
		{
			foreach (var timer in _timers)
			{
				timer.Save();
			}
		}

		public void Tick()
		{
			foreach (var timer in _timers)
			{
				timer.Update();
			}
		}
	}
}
