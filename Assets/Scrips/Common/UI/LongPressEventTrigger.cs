using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scrips.Common.UI
{
	public class LongPressEventTrigger: UIBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
	{
		[Tooltip("How long must pointer be down on this object to trigger a long press")]
		public float durationThreshold = 1f;

		[Tooltip("How long must pointer be down on this object to trigger a long press")]
		public float clickInterval = 0.3f;

		public UnityEvent onLongPress = new UnityEvent();

		private bool isPointerDown = false;
		
		private bool longPressTriggered = false;
		
		private float timePressStarted;

		private float clickLeftTime;


		private void Update()
		{
			if (isPointerDown && !longPressTriggered)
			{
				if (Time.time - timePressStarted > durationThreshold)
				{
					longPressTriggered = true;
				}
			}

			if (isPointerDown && longPressTriggered)
			{
				clickLeftTime -= Time.deltaTime;
				if (clickLeftTime < 0)
				{
					clickLeftTime = clickInterval;
					onLongPress.Invoke();
				}
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			timePressStarted = Time.time;
			isPointerDown = true;
			longPressTriggered = false;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			isPointerDown = false;
		}


		public void OnPointerExit(PointerEventData eventData)
		{
			isPointerDown = false;
		}
	}
}
