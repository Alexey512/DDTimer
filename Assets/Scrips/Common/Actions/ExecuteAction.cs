using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{
	public class ExecuteAction: BaseAction
	{
		private readonly Action _action;
		
		public ExecuteAction(Action action)
		{
			_action = action;
		}
		
		protected override void OnExecute()
		{
			_action?.Invoke();

			Status = ActionStatus.Finished;
		}
	}
}
