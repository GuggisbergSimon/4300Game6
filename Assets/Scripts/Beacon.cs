﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
	[SerializeField]private GameObject ink;
	private bool _isActivated = false;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !_isActivated)
		{
			_isActivated = true;
			gameObject.SetActive(false);
			Instantiate(ink, transform.position, Quaternion.identity);
		}
	}
}
