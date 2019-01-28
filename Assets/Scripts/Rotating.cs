using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
	[SerializeField] private float minRotationConstantSpeed = 1.0f;
	[SerializeField] private float maxRotationConstantSpeed = 2.0f;
	private float _rotationConstantSpeed;

	private void Start()
	{
		_rotationConstantSpeed = Random.Range(minRotationConstantSpeed, maxRotationConstantSpeed);
	}

	private void Update()
	{
		transform.Rotate(0, 0, _rotationConstantSpeed * Time.deltaTime);
	}
}