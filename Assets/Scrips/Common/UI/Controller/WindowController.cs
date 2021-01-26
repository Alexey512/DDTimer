using System;
using Assets.Scrips.Common.Actions;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Common.UI.Controller
{
	public class WindowController: MonoBehaviour, IWindowController
    {
		public WindowMode Mode { get; protected set; } = WindowMode.Single;
		
		public Transform Owner => this.transform;

        protected IUIManager Manager;

        protected IActionsSequencer ActionsSequencer;

        protected IAction ShowAction;

        protected IAction HideAction;

        [Inject]
        protected void Construct(IUIManager uiManager, IActionsSequencer sequencer)
        {
            Manager = uiManager;
            ActionsSequencer = sequencer;

			gameObject.SetActive(false);

            OnInit();
        }

		public void Open(Action<IWindowController> callback = null)
		{
			gameObject.SetActive(true);

			if (ShowAction != null)
			{
				ActionsSequencer.Execute(ShowAction, action =>
				{
					OnOpen();
				});
			}
			else
			{
				OnOpen();
			}
		}

		public void Close(Action<IWindowController> callback = null)
        {
	        if (HideAction != null)
	        {
		        ActionsSequencer.Execute(HideAction, action =>
		        {
			        CloseImpl(callback);
		        });
	        }
	        else
	        {
		        CloseImpl(callback);
	        }			
        }

		private void CloseImpl(Action<IWindowController> callback = null)
		{
			gameObject.SetActive(false);
			OnClose();
			Manager.Close(this);
			callback?.Invoke(this);
		}

		private void Update()
		{
			OnUpdate();
		}

		protected virtual void OnInit()
		{

		}

        protected virtual void OnOpen()
        {

        }

        protected virtual void OnClose()
        {

        }

        protected virtual void OnUpdate()
        {

        }
    }
}
