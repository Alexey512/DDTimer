using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.Actions;
using Assets.Scrips.Common.Actions.Animator;
using Assets.Scrips.Common.UI;
using Assets.Scrips.Game.Timers;
using Assets.Scripts.Common.UI.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scrips.Game.UI
{
	public class TimerWindow: WindowController
	{
		[SerializeField]
		private TextMeshProUGUI _label;

		[SerializeField]
		private Button _btnAdd;

		[SerializeField] 
		private LongPressEventTrigger _longPressAdd;

		[SerializeField]
		private Button _btnRem;

		[SerializeField] 
		private LongPressEventTrigger _longPressRem;

		[SerializeField]
		private Button _btnPause;

		[SerializeField]
		private TextMeshProUGUI _btnPauseLabel;

		[SerializeField]
		private Button _btnBack;

		[SerializeField]
		private int _incrementValue = 1;

		[SerializeField]
		private Animator _animator;

		[Inject]
		private ITimersController _timersController;

		private TimerEntity _timer;

		protected override void OnInit()
		{
			InitButtons();
			InitActions();
		}

		private void InitActions()
		{
			var showAction = new SequenceAction();
			showAction.Add(new SetAnimatorBool(_animator, "IsShow", true));
			showAction.Add(new WaitAnimatorState(_animator, "Open"));
			ShowAction = showAction;

			var hideAction = new SequenceAction();
			hideAction.Add(new SetAnimatorBool(_animator, "IsShow", false));
			hideAction.Add(new WaitAnimatorState(_animator, "Close"));
			HideAction = hideAction;
		}

		private void InitButtons()
		{
			_btnAdd.onClick.AddListener(() =>
			{
				AddTime(_incrementValue);
			});
			_longPressAdd.onLongPress.AddListener(() =>
			{
				AddTime(_incrementValue);
			});

			_btnRem.onClick.AddListener(() =>
			{
				AddTime(-_incrementValue);
			});
			_longPressRem.onLongPress.AddListener(() =>
			{
				AddTime(_incrementValue);
			});

			_btnPause.onClick.AddListener(() =>
			{
				if (_timer != null)
				{
					_timer.IsPaused = false;
				}

				Close();
			});

			_btnBack.onClick.AddListener(() =>
			{
				Close();
			});
		}

		public void Initialize(int timerIndex)
		{
			_timer = _timersController.GetTimer(timerIndex);
		}

		private void AddTime(int value)
		{
			if (_timer == null)
				return;

			_timer.AddTime(value);
		}

		private void UpdateTimerLabel()
		{
			if (_timer == null)
				return;

			_label.text = TimeSpan.FromSeconds(_timer.GetLeftTime()).ToString(@"hh\:mm\:ss");
		}

		protected override void OnUpdate()
		{
			UpdateTimerLabel();
		}

		protected override void OnClose()
		{
			Manager.Open<MainMenu>();
		}
	}
}
