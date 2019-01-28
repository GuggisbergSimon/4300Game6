using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
	[SerializeField] private GameObject inkPrefab = null;
	private bool _isInked = false;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!_isInked && other.CompareTag("Player"))
		{
			GameManager.Instance.Player.BeaconActivated(gameObject);
			_isInked = true;
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}

			Instantiate(inkPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
		}
	}
}