using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class UIManager : MonoBehaviour
{
	[SerializeField] private PostProcessVolume vignettePostProcessVolume = null;
	[SerializeField] private float vignetteTransitionTime = 1.0f;
	private float _initWeightVignette;
	private Coroutine _vignetteCoroutine;

	private void Start()
	{
		_initWeightVignette = vignettePostProcessVolume.weight;
	}

	public void ToggleVignette(bool value)
	{
		if (_vignetteCoroutine != null)
		{
			StopCoroutine(_vignetteCoroutine);
		}
		_vignetteCoroutine = StartCoroutine(TogglingVignette(value, vignetteTransitionTime));
	}

	private IEnumerator TogglingVignette(bool value, float time)
	{
		float weight = value ? 1.0f : _initWeightVignette;
		float timer = 0.0f;
		float initWeight = vignettePostProcessVolume.weight;
		while (timer < time)
		{
			vignettePostProcessVolume.weight = Mathf.Lerp(initWeight, weight, timer / time);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}
