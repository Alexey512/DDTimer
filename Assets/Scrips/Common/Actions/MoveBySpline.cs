using DG.Tweening;
using UnityEngine;

namespace Assets.Scrips.Common.Actions
{
	public class MoveBySpline: BaseAction
	{
		private readonly Transform _target;
		private readonly Vector3[] _path;
		private readonly float _duration;

		public MoveBySpline(Transform target, Vector3[] path, float duration)
		{
			_target = target;
			_path = path;
			_duration = duration;
		}

		protected override void OnExecute()
		{
			if (_target == null || _path.Length == 0)
			{
				Status = ActionStatus.Finished;
				return;
			}

			float distance = 0;
			for (int i = 0; i < _path.Length - 1; i++)
			{
				distance += Vector3.Distance(_path[i], _path[i + 1]);
			}

			float speed = _duration;
			float time = distance / speed;
			_target.DOPath(_path, time, PathType.Linear).SetEase(Ease.Linear).onComplete += () => { Status = ActionStatus.Finished; };
		}
	}
}
