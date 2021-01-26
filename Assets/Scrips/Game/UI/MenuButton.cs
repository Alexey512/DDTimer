using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.UI;
using Assets.Scrips.Game.Timers;
using TMPro;
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
		private TextMeshProUGUI _buttonLabel;

		[SerializeField]
		private Animator _animator;

		[SerializeField]
		private string _closeStateName = "Close";

		[SerializeField]
		private string _openStateName = "Open";

		[SerializeField]
		private string _stateName = "IsShow";

		private Action<int> _callback;

		private TimerEntity _timer;

		public int Index => _timer.Index;

		private void Awake()
		{
			_button.onClick.AddListener(() =>
			{
				_callback.Invoke(Index);
			});
		}

		public void Initialize(TimerEntity timer, Action<int> callback)
		{
			_timer = timer;
			_callback = callback;

			_buttonLabel.text = $"Button {_timer.Index + 1}";

			_timer.Complete += OnTimerComplete;

			UpdateButtonState();
		}

		private void OnTimerComplete(TimerEntity timer)
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
			_buttonLabel.color = _timer.IsComplete() ? Color.green : Color.black;
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

		private void OnDestroy()
		{
			if (_timer != null)
			{
				_timer.Complete -= OnTimerComplete;
			}
		}
	}
}
