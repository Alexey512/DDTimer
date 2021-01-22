using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{
	public interface IActionsSequencer
	{
		void Execute(IAction action, Action<IAction> callback = null);
		
		void Remove(IAction action);
	}
}
