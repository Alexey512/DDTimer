using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scrips.Common.Actions;
using Assets.Scrips.Game.Timers;
using Assets.Scrips.UI;
using Assets.Scripts.Common.UI.Controller;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scrips.Game.UI
{
    public class MainMenu: WindowController
    {
	    public class ButtonsContainer: SlotsContainer<MenuButton> {}

	    [SerializeField]
	    private MenuButton _buttonPref;

	    [SerializeField]
	    private Transform _container;

	    private ButtonsContainer _buttons = new ButtonsContainer();

	    [Inject] 
	    private ITimersController _timersController;

	    private int _selectTimer = -1;

	    protected override void OnInit()
	    {
		    _buttons.Initialize(_buttonPref, _container);

			var showAction = new ParallelAction();
			var showActions = new SequenceAction();
			showAction.Add(showActions);

			var hideAction = new ParallelAction();
			var hideActions = new SequenceAction();
			hideAction.Add(hideActions);

		    for (int i = 0; i < _timersController.TimersCount; i++)
		    {
			    var btn = _buttons.AddSlot();
			    if (btn == null)
			    {
				    return;
			    }

			    btn.Initialize(_timersController.GetTimer(i), OnButtonClick);

			    showActions.Add(new ExecuteAction(() =>
			    {
					btn.Open();
			    }));
			    showActions.Add(new WaitAction(0.15f));

			    hideActions.Add(new ExecuteAction(() =>
			    {
				    btn.Close();
			    }));
			    hideActions.Add(new WaitAction(0.15f));
		    }

		    showAction.Add(new CallbackAction(finish =>
		    {
			    foreach (var btn in _buttons.Slots)
			    {
				    if (btn.GetState() != MenuButton.State.Open)
						return;
			    }

			    finish();
		    }));

		    showAction.Add(new CallbackAction(finish =>
		    {
			    foreach (var btn in _buttons.Slots)
			    {
				    if (btn.GetState() != MenuButton.State.Close)
					    return;
			    }

			    finish();
		    }));

		    ShowAction = showAction;
		    HideAction = hideAction;
	    }

	    private void OnButtonClick(int index)
	    {
		    _selectTimer = index;
		    
		    Close();
	    }

	    protected override void OnClose()
	    {
		    if (_selectTimer >= 0)
		    {
				Manager.Open<TimerWindow>().Initialize(_selectTimer);
		    }
	    }

	    private void OnDestroy()
	    {
	    }
    }
}
