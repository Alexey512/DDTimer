using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scrips.Common.Actions
{
	public class MoveBySpeed: BaseAction
	{
		private readonly Transform _target;
		private readonly Vector3 _position;
		private readonly float _speed;

		public MoveBySpeed(Transform target, Vector3 position, float speed)
		{
			_target = target;
			_position = position;
			_speed = speed;
		}

		protected override void OnExecute()
		{
			if (_target == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			if (Math.Abs(_speed) < float.Epsilon)
			{
				_target.position = _position;
				Status = ActionStatus.Finished;
				return;
			}

			float duration = Vector3.Distance(_target.position, _position) / _speed;
			_target.DOMove(_position, duration).onComplete += () => { Status = ActionStatus.Finished; };
		}
	}
}
