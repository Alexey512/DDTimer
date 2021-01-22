using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace Assets.Scrips.Common.Actions
{
	public class MoveToAction: BaseAction
	{
		private readonly Transform _target;
		private readonly Vector3 _position;
		private readonly float _duration;

		public MoveToAction(Transform target, Vector3 position, float duration)
		{
			_target = target;
			_position = position;
			_duration = duration;
		}

		protected override void OnExecute()
		{
			if (_target == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			_target.DOMove(_position, _duration).onComplete += () => { Status = ActionStatus.Finished; };
		}
	}
}
