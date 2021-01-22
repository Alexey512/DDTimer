using System;
using Assets.Scripts.Common.UI.Controller;

namespace Assets.Scripts.Common.UI
{
	public interface IUIManager
	{
		event Action<IWindowController> WindowOpen;
	
		event Action<IWindowController> WindowPreClose;

		event Action<IWindowController> WindowClose;

		T Open<T>() where T : class, IWindowController;

		T GetWindow<T>() where T : class, IWindowController;

		void Close(IWindowController controller);

		void CloseAll();

		bool HasOpenedWindows();

		int GetWindowsCount();
	}
}
