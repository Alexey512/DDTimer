using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scrips.Common.Actions
{
	public class ActionsSequencer: IActionsSequencer, ITickable
	{
		private List<Tuple<IAction, Action<IAction>>> _actions = new List<Tuple<IAction, Action<IAction>>>();
		
		public void Execute(IAction action, Action<IAction> callback = null)
		{
			if (action == null)
			{
				return;
			}

			if (_actions.Exists(a => a.Item1 == action))
			{
				return;
			}

			_actions.Add(new Tuple<IAction, Action<IAction>>(action, callback));
			action.Execute();
		}

		public void Remove(IAction action)
		{
			int index = _actions.FindIndex(a => a.Item1 == action);
			if (index < 0)
				return;
			_actions.RemoveAt(index);
		}

		public void Tick()
		{
			
			for (int i = _actions.Count - 1; i >= 0; i--)
			{
				_actions[i].Item1.Update();
				
				if (_actions[i].Item1.Status == ActionStatus.Finished)
				{
					_actions[i].Item2?.Invoke(_actions[i].Item1);
					_actions.RemoveAt(i);
				}
			}
		}
	}
}
