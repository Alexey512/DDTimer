using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scrips.Common.Actions
{
	public class WaitAction: BaseAction
	{
		private readonly float _delay;

		private float _leftTime;

		public WaitAction(float delay)
		{
			_delay = delay;
		}

		protected override void OnExecute()
		{
			_leftTime = _delay;
			Status = ActionStatus.Active;
		}

		public override void Update()
		{
			if (Status != ActionStatus.Active)
				return;

			_leftTime -= Time.deltaTime;
			if (_leftTime <= 0)
			{
				Status = ActionStatus.Finished;
			}
		}
	}
}
