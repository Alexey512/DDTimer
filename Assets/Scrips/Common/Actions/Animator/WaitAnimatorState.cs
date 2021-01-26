using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions.Animator
{
	public class WaitAnimatorState: BaseAction
	{
		private readonly UnityEngine.Animator _animator;
		
		private readonly string _tagName;

		public WaitAnimatorState(UnityEngine.Animator animator, string tagName)
		{
			_animator = animator;
			_tagName = tagName;
		}

		private void CheckState()
		{
			if (_animator == null)
				return;

			if (_animator.GetCurrentAnimatorStateInfo(0).IsTag(_tagName))
			//var transitionInfo = _animator.GetAnimatorTransitionInfo(0);
			//if (transitionInfo.IsName(_tagName))
			{
				Status = ActionStatus.Finished;
			}
		}

		protected override void OnExecute()
		{
			if (_animator == null)
			{
				Status = ActionStatus.Finished;
				return;
			}

			CheckState();
		}

		public override void Update()
		{
			CheckState();
		}
	}
}
