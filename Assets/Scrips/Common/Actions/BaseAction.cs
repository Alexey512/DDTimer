using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{
	public abstract class BaseAction: IAction
	{
		public ActionStatus Status { get; protected set; } = ActionStatus.Inactive;

		public void Execute()
		{
			if (Status != ActionStatus.Inactive)
				return;

			OnExecute();
		}

		public virtual void Update()
		{

		}

		public virtual void Finish()
		{

		}

		protected virtual void OnExecute()
		{

		}
	}
}
