using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{

	public class ExecuteWhileAction: BaseAction
	{
		private readonly Func<ActionStatus> _callback;
		
		public ExecuteWhileAction(Func<ActionStatus> callback)
		{
			_callback = callback;
		}
		
		protected override void OnExecute()
		{
			if (_callback == null)
			{
				Status = ActionStatus.Finished;
			}
		}

		public override void Update()
		{
			var result = _callback?.Invoke();
			if (result == ActionStatus.Finished)
			{
				Status = ActionStatus.Finished;
			}
		}
	}
}
