using System;
using UnityEngine;
using Assets.Scrips.Common.Visual;
using Assets.Scrips.Common.Actions;
using TMPro;
using Zenject;

namespace Assets.Scrips.HOG.Visual
{
	public class EffectsHelper
	{
		[Inject]
		private readonly VisualRoot _visualRoot;
		[Inject]
		private readonly IPrefabsFactory _factory;
		[Inject]
		private readonly IVisualFactory _visualFactory;
		[Inject]
		private readonly IActionsSequencer _actionsSequencer;

		public void ShowCubeBubble(Vector3 position, float flyHeight)
		{
			var effectObj = _factory.Create("CubeClickBubble");
			effectObj.transform.parent = _visualRoot.Root;
			effectObj.transform.position = position;

			var effAction = new SequenceAction();
			effAction.Add(new MoveToAction(effectObj.transform, new Vector3(position.x, position.y + flyHeight, position.z), 0.7f));
			effAction.Add(new FadeToAction(effectObj, 1f, 0f, 0.2f));

			effAction.Add(new ExecuteAction(() => 
			{
				_factory.Remove(effectObj);
			}));

			_actionsSequencer.Execute(effAction);
		}
	}
}
