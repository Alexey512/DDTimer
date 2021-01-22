using System;
using UnityEngine;

namespace Assets.Scrips.Common.InputSystem
{
	public interface IInputManager
	{
		event Action<Vector3> MouseDown;
		
		event Action<Vector3> MouseClick;

		event Action<Vector3> MouseDoubleClick;
		
		event Action<Vector3> MouseUp;
		
		event Action<Vector3, Vector3> MouseMove;

		event Action<float> MouseWheel;

		bool IsLocked { get; }

		void Lock();
		void Unlock();

		void LockGuiInput();
		void UnlockGuiInput();
	}
}
