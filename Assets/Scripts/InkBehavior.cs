using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InkBehavior : MonoBehaviour
{
	[SerializeField] private float time;
	[SerializeField] private Vector2 maxScale;

	private void Start()
	{
		StartCoroutine(InkExplosion(time));
	}

	private IEnumerator InkExplosion(float time)
	{
		float timer = 0.0f;
		while (true)
		{
			transform.localScale = Vector3.Lerp(Vector2.zero, maxScale, timer / time);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}