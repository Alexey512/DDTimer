using System;
using UnityEngine;

namespace Assets.Scripts.Common.UI.Controller
{
	public enum WindowMode
	{
		Single,
		Modal,
	}
	
	public interface IWindowController
	{
		WindowMode Mode { get; }
		
		Transform Owner { get; }

		void Open(Action<IWindowController> callback = null);

		void Close(Action<IWindowController> callback = null);
	}
}
