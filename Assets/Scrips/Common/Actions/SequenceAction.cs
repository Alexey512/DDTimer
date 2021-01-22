using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{
	public sealed class SequenceAction: CompositeAction
	{
		private IAction _currentAction;
		
		protected override void OnExecute()
		{
			if (!NextAction())
			{
				Status = ActionStatus.Finished;
			}
			else
			{
				Status = ActionStatus.Active;
			}
		}

		public override void Update()
		{
			if (_currentAction == null)
				return;

			_currentAction.Update();

			if (_currentAction.Status == ActionStatus.Finished)
			{
				if (!NextAction())
				{
					Status = ActionStatus.Finished;
				}
			}
		}

		private bool NextAction()
		{
			if (Childs.Count == 0)
				return false;

			if (_currentAction != null)
			{
				int index = Childs.IndexOf(_currentAction);
				if (index < 0 || index + 1 >= Childs.Count)
					return false;
				_currentAction = Childs[index + 1];
				_currentAction.Execute();
			}
			else
			{
				_currentAction = Childs[0];
				_currentAction.Execute();
			}
			return true;
		}

		public override void Finish()
		{
			base.Finish();
		}
	}
}
