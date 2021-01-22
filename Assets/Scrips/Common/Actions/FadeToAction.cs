using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scrips.Common.Actions
{
	public class FadeToAction: BaseAction
	{
		private SpriteRenderer[] _spriteRenderers;
		private TextMeshPro[] _texts;
		private Image[] _images;

		private float _from;
		private float _to;
		private float _duration;
		private float _currAlpha;
		private readonly Ease _ease;

		public FadeToAction(GameObject owner, float from, float to, float duration, Ease ease = Ease.Linear)
		{
			_from = from;
			_to = to;
			_duration = duration;
			_ease = ease;
			_spriteRenderers = owner.GetComponentsInChildren<SpriteRenderer>();
			_texts = owner.GetComponentsInChildren<TextMeshPro>();
			_images = owner.GetComponentsInChildren<Image>();
		}

		protected override void OnExecute()
		{
			SetFade(_from);

			if (Mathf.Abs(_duration) < 0.0001f)
			{
				SetFade(_to);
				Status = ActionStatus.Finished;
				return;
			}

			DOTween.To(() => _currAlpha, SetFade, _to, _duration).SetEase(_ease).onComplete += () => { Status = ActionStatus.Finished; };
		}

		private void SetFade(float alpha)
		{
			Color color = Color.white;
			color.a = alpha;
			foreach (var renderer in _spriteRenderers)
			{
				renderer.material.color = color;
			}

			foreach (var textMesh in _texts)
			{
				if (textMesh.textInfo.wordInfo[0].textComponent == null)
					continue;
				textMesh.textInfo.wordInfo[0].textComponent.alpha = alpha;
			}

			foreach (var image in _images)
			{
				image.color = color;
			}

			_currAlpha = alpha;
		}
	}
}
