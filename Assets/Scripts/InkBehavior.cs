using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InkBehavior : MonoBehaviour
{
	[SerializeField] private float minTimeGrowing = 1.0f;
	[SerializeField] private float maxTimeGrowing = 2.0f;
	[SerializeField] private float minScaleAfterGrowing = 3.0f;
	[SerializeField] private float maxScaleAfterGrowing = 5.0f;
	[SerializeField] private int minInkSpawned = 2;
	[SerializeField] private int maxInkSpawned = 5;
	[SerializeField] private GameObject inkPrefab = null;
	[SerializeField] private Color[] colors = null;
	[SerializeField] private Sprite[] sprites = null;
	private float _timeGrowing;
	private float _scaleAfterGrowing;
	private int _numberInkSpawned;
	private SpriteRenderer _mySprite;

	private void Start()
	{
		_mySprite = GetComponentInChildren<SpriteRenderer>();
		_mySprite.sprite = sprites[Random.Range(0, sprites.Length)];
		_mySprite.color = colors[Random.Range(0, colors.Length)];
		_timeGrowing = Random.Range(minTimeGrowing, maxTimeGrowing);
		_scaleAfterGrowing = Random.Range(minScaleAfterGrowing, maxScaleAfterGrowing);
		_numberInkSpawned = Random.Range(minInkSpawned, maxInkSpawned);
		StartCoroutine(InkGrowing(_timeGrowing));
	}

	private IEnumerator InkGrowing(float time)
	{
		float timer = 0.0f;
		while (timer < time)
		{
			transform.localScale = Vector3.Lerp(Vector2.zero,
				Vector2.one * _scaleAfterGrowing, timer / time);
			timer += Time.deltaTime;
			yield return null;
		}

		for (int i = 0; i < _numberInkSpawned; i++)
		{
			float width = 3.0f;
			GameObject ink = Instantiate(inkPrefab,
				transform.position + width * _scaleAfterGrowing * new Vector3(Random.Range(-1,1), Random.Range(-1,1), 0),
				Quaternion.Euler(0, 0, Random.Range(0, 360)), transform.parent);
			ink.GetComponentInChildren<SpriteRenderer>().sortingOrder = _mySprite.sortingOrder + i + 1;
		}
	}
}