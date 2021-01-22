using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.Visual;

namespace Assets.Scrips.Common.Actions
{
	public class PlayParticleEffectAction: BaseAction
	{
		private readonly string _effectId; 
		private readonly Vector3 _position;
		private readonly Transform _owner; 
		private readonly IPrefabsFactory _factory;
		private readonly float _duration;

		private ParticleSystem _particle;
		private float _leftTime;

		public PlayParticleEffectAction(string effectId, Vector3 position, Transform owner, float duration, IPrefabsFactory factory)
		{
			_effectId = effectId; 
			_owner = owner; 
			_factory = factory;
			_position = position;
			_duration = duration;
		}

		protected override void OnExecute()
		{
			var effObj = _factory.Create(_effectId);
			if (effObj == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			effObj.transform.parent = _owner;
			effObj.transform.position = _position;

			_particle = effObj.GetComponent<ParticleSystem>();
			if (_particle == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			_leftTime = _duration;

			Status = ActionStatus.Active;
		}

		public override void Update()
		{
			if (_particle == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			if (_duration > 0)
			{
				_leftTime -= Time.deltaTime;
				if (_leftTime <= 0)
				{
					_particle.Stop();
				}
			}
			
			if (!_particle.IsAlive())
			{
				_factory.Remove(_particle.gameObject);
				_particle = null;
				Status = ActionStatus.Finished;
			}
		}
	}
}
