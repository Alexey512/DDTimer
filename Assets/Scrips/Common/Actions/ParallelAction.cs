using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{
	public class ParallelAction: CompositeAction
	{
		protected override void OnExecute()
		{
			foreach (var child in Childs)
			{
				child.Execute();
			}

			Status = ActionStatus.Active;
		}

		public override void Update()
		{
			if (Status != ActionStatus.Active)
				return;
			
			Status = ActionStatus.Finished;
			
			foreach (var child in Childs)
			{
				child.Update();
				if (child.Status != ActionStatus.Finished)
				{
					Status = ActionStatus.Active;
				}
			}
		}

		public override void Finish()
		{
			base.Finish();
		}
	}
}
