using System;
using Assets.Scripts.Common.UI.Controller;
using System.Collections.Generic;

namespace Assets.Scripts.Common.UI
{
	public class UIManager: IUIManager
	{
		public event Action<IWindowController> WindowOpen;
	
		public event Action<IWindowController> WindowPreClose;

		public event Action<IWindowController> WindowClose;
		
		private readonly IWindowFactory _factory;

		private readonly IWindowRoot _root;

	    private List<IWindowController> _windows = new List<IWindowController>();

		private IWindowController _modalWindow;

		public UIManager(IWindowRoot root, IWindowFactory factory)
		{
			_factory = factory;
			_root = root;
		}

		public T Open<T>() where T : class, IWindowController
		{
			var window = _factory.Create<T>();
			if (window == null)
				return null;

		    window.Owner.SetParent(_root.Root, false);

			//TODO: add modal, single types
			if (window.Mode == WindowMode.Single)
			{
				_windows.Add(window);
				window.Open();
			}
			else if (window.Mode == WindowMode.Modal)
			{
				if (_modalWindow != null)
				{
					_modalWindow.Close();
				}
				_modalWindow = window;
				window.Open();
			}

			WindowOpen?.Invoke(window);

			return window;
		}

		public T GetWindow<T>() where T : class, IWindowController
		{
			foreach (var window in _windows)
			{
				var wnd = window as T;
				if (wnd != null)
					return wnd;
			}
			
			return null;
		}

		public void Close(IWindowController window)
		{
			if (window == null)
				return;

			WindowPreClose?.Invoke(window);

			_windows.Remove(window);
			if (_modalWindow == window)
			{
				_modalWindow = null;
			}

			//TODO: stackowerflow
			//window.Close();

			_factory.Remove(window);

			WindowClose?.Invoke(window);
		}

		public void CloseAll()
		{
			while (_windows.Count > 0)
			{
				_windows[0].Close();
			}
		}

		public int GetWindowsCount()
		{
			return _windows.Count;
		}

		public bool HasOpenedWindows()
		{
			return _windows.Count > 0;
		}
	}
}
