using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrips.Common.Actions
{
	public abstract class CompositeAction : BaseAction, ICollection<IAction>
	{
		public List<IAction> _childs = new List<IAction>();

		public List<IAction> Childs => _childs;

		public IEnumerator<IAction> GetEnumerator()
		{
			return _childs.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_childs).GetEnumerator();
		}

		public void Add(IAction item)
		{
			_childs.Add(item);
		}

		public void Clear()
		{
			_childs.Clear();
		}

		public bool Contains(IAction item)
		{
			return _childs.Contains(item);
		}

		public void CopyTo(IAction[] array, int arrayIndex)
		{
			_childs.CopyTo(array, arrayIndex);
		}

		public bool Remove(IAction item)
		{
			return _childs.Remove(item);
		}

		public int Count => _childs.Count;

		public bool IsReadOnly => ((ICollection<IAction>)_childs).IsReadOnly;
	}
}
