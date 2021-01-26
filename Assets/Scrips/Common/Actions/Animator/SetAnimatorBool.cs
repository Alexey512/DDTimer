using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrips.Common.Actions.Animator
{
	public class SetAnimatorBool: BaseAction
	{
		private readonly UnityEngine.Animator _animator;
		
		private readonly string _varName;

		private readonly bool _varValue;

		public SetAnimatorBool(UnityEngine.Animator animator, string varName, bool varValue)
		{
			_animator = animator;
			_varName = varName;
			_varValue = varValue;
		}

		protected override void OnExecute()
		{
			if (_animator == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			_animator.SetBool(_varName, _varValue);
			
			Status = ActionStatus.Finished;
		}
	}
}
