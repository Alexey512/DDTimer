using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrips.UI
{
	public class SlotsContainer<T> where T: MonoBehaviour
	{
		[SerializeField]
		private T _slotPref;

		[SerializeField]
		private Transform _container;

		private readonly List<T> _slots = new List<T>();

		public List<T> Slots => _slots;

		public void Initialize(T slotPref, Transform container)
		{
			_slotPref = slotPref;
			_container = container;
		}

		public T AddSlot()
		{
			var obj = GameObject.Instantiate(_slotPref.gameObject, _container);
			obj.SetActive(true);
			T slot = obj.GetComponent<T>();
			_slots.Add(slot);
			return slot;
		}

		public void Clear()
		{
			foreach (var slot in _slots)
			{
				GameObject.Destroy(slot.gameObject);
			}

			_slots.Clear();
		}
	}
}
