using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconFinder : MonoBehaviour
{
	[SerializeField] private float changeTime = 1.0f;
	private Vector2 _closestBeaconPosition;
	private Vector2 _currentPos;
	private Coroutine _movingCoroutine;

	private void Start()
	{
		_closestBeaconPosition = GameManager.Instance.BeaconsLeft[0].transform.position;
	}

	private void Update()
	{
		Scan();
		transform.parent.up = (Vector3) _currentPos - GameManager.Instance.Player.transform.position;
	}

	private void Scan()
	{
		foreach (var beacon in GameManager.Instance.BeaconsLeft)
		{
			if ((GameManager.Instance.Player.transform.position - (Vector3) _closestBeaconPosition).magnitude >
			    (GameManager.Instance.Player.transform.position - beacon.transform.position).magnitude)
			{
				_closestBeaconPosition = beacon.transform.position;
				if (_movingCoroutine != null)
				{
					StopCoroutine(_movingCoroutine);
				}

				_movingCoroutine = StartCoroutine(ChangePosition(_closestBeaconPosition, changeTime));
			}
		}
	}

	public void ReScan()
	{
		_closestBeaconPosition = new Vector2(10000, 10000);
		Scan();
	}

	private IEnumerator ChangePosition(Vector2 position, float time)
	{
		Vector2 initPos = transform.position;
		float timer = 0.0f;
		while (timer < time)
		{
			_currentPos = Vector2.Lerp(initPos, _closestBeaconPosition, timer / time);
			timer += Time.deltaTime;
			yield return null;
		}
	}
}