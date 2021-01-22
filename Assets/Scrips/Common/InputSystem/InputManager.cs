using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scrips.Common.InputSystem
{
	public sealed class InputManager: MonoBehaviour, IInputManager
    {
        public event Action<Vector3> MouseDown;
		public event Action<Vector3> MouseClick;
		public event Action<Vector3> MouseDoubleClick;
		public event Action<Vector3> MouseUp;
		public event Action<Vector3, Vector3> MouseMove;
		public event Action<float> MouseWheel;

		private const float DeltaRegistration = 0.01f;
        private bool _isMouseDown;
        private bool _isOneClick;
        private bool _isMouseMove;
        private float _doubleClickTime;
        private const float Delay = 0.6f;
        private Vector3? _previousMousePosition = null;

        private Vector3? _buttonDownPosition = null;

        public bool IsLocked { get; private set; }

        public void Lock()
        {
            if (IsLocked)
                return;

            IsLocked = true;

            _isMouseDown = false;
            _isMouseMove = false;
            _isOneClick = false;
            _previousMousePosition = null;

            LockGuiInput();
        }

        public void Unlock()
        {
	        if (!IsLocked)
		        return;

	        IsLocked = false;

	        UnlockGuiInput();
        }

        public void LockGuiInput()
        {
	        var raycaster =  GameObject.FindObjectOfType<GraphicRaycaster>();
	        if (raycaster != null)
		        raycaster.enabled = false;
		}

        public void UnlockGuiInput()
        {
	        var raycaster =  GameObject.FindObjectOfType<GraphicRaycaster>();
	        if (raycaster != null)
		        raycaster.enabled = true;
        }

        private void Update()
        {
            if (IsLocked)
                return;
	        
	        var mousePosition = Input.mousePosition;

            if (IsOverGui())
            {
                _isMouseDown = false;
				_isMouseMove = false;
                _isOneClick = false;
                _previousMousePosition = null;
                return;
            }

			float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
			if (mouseWheel != 0)
			{
				MouseWheel?.Invoke(mouseWheel);
			}

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _isMouseDown = true;
				_isMouseMove = false;
                _buttonDownPosition = mousePosition;
				_previousMousePosition = null;

                if (!_isOneClick)
                {
                    _isOneClick = true;
                    _doubleClickTime = Time.time;
					MouseDown?.Invoke(mousePosition);
                    IsOverGui();
                }
                else
                {
                    _isOneClick = false;
                    MouseDoubleClick?.Invoke(mousePosition);
					_buttonDownPosition = mousePosition;
                    return;
                }
            }

            if (_isOneClick && Time.time - _doubleClickTime > Delay)
            {
                _isOneClick = false;
            }

            if (!_isMouseDown)
                return;

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _isMouseDown = false;
				MouseUp?.Invoke(mousePosition);
				if (!_isMouseMove)
				{
					MouseClick?.Invoke(mousePosition);
					_isMouseMove = false;
				}
				_buttonDownPosition = null;
				_previousMousePosition = null;
                return;
            }
            
			if (_buttonDownPosition != null)
			{
				if (_previousMousePosition != null)
				{
					if (Vector3.Distance(_previousMousePosition.Value, mousePosition) > DeltaRegistration)
					{
						_isOneClick = false;
						_isMouseMove = true;
						MouseMove?.Invoke(mousePosition, _previousMousePosition.Value);
						_previousMousePosition = mousePosition;
					}
				}
				else
				{
					if (Vector3.Distance(_buttonDownPosition.Value, mousePosition) > DeltaRegistration)
					{
						_isOneClick = false;
						_isMouseMove = true;
						MouseMove?.Invoke(mousePosition, _buttonDownPosition.Value);
						_previousMousePosition = mousePosition;
					}
				}
			}
        }

        private bool IsOverGui()
        {
	        if (EventSystem.current.IsPointerOverGameObject())
                return true;

	        //if (IsPointerOverUIObject())
		     //   return true;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return true;
            }

            return false;
        }

        private bool IsPointerOverUIObject() 
        {
	        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
	        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	        List<RaycastResult> results = new List<RaycastResult>();
	        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
	        return results.Count > 0;
        }
    }
}
