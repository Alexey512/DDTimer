using System;

namespace Assets.Scrips.Common.Actions
{
	public class CallbackAction: BaseAction
	{
		private readonly Action<Action> _action;
		
		public CallbackAction(Action<Action> action)
		{
			_action = action;
		}
		
		protected override void OnExecute()
		{
			_action?.Invoke(() =>
			{
				Status = ActionStatus.Finished;
			});
		}
	}
}
