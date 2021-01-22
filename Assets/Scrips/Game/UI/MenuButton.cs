using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Assets.Scrips.Game.UI
{
	public class MenuButton: MonoBehaviour
	{
		public enum State
		{
			None,
			Close,
			Open
		}

		[SerializeField]
		private Button _button;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private string _closeStateName = "Close";

		[SerializeField]
		private string _openStateName = "Open";

		[SerializeField]
		private string _stateName = "IsShow";

		private Action<int> _callback;

		public int Index { get; private set; }

		private void Awake()
		{
			_button.onClick.AddListener(() =>
			{
				_callback.Invoke(Index);
			});
		}

		public void Initialize(int index, Action<int> callback)
		{
			Index = index;
			_callback = callback;
		}

		public void Open()
		{
			if (_animator == null)
				return;

			_animator.SetBool(_stateName, true);
		}

		public void Close()
		{
			if (_animator == null)
				return;

			_animator.SetBool(_stateName, false);
		}

		public State GetState()
		{
			if (_animator == null)
			{
				return State.None;
			}

			var transitionInfo = _animator.GetAnimatorTransitionInfo(0);
			if (transitionInfo.IsUserName(_closeStateName))
			{
				return State.Close;
			}
			if (transitionInfo.IsUserName(_openStateName))
			{
				return State.Open;
			}

			return State.None;
		}
	}
}
