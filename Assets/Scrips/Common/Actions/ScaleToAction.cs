using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scrips.Common.Actions
{
	public class ScaleToAction: BaseAction
	{
		private readonly Transform _target;
		private readonly Vector3 _scale;
		private readonly float _duration;
		private readonly Ease _ease;

		public ScaleToAction(Transform target, Vector3 scale, float duration, Ease ease = Ease.Linear)
		{
			_target = target;
			_scale = scale;
			_duration = duration;
			_ease = ease;
		}

		protected override void OnExecute()
		{
			if (_target == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			_target.DOScale(_scale, _duration).SetEase(_ease).onComplete += () => { Status = ActionStatus.Finished; };
		}
	}
}
