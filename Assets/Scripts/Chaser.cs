﻿using UnityEngine;

public class Chaser : MonoBehaviour
{
	[SerializeField] private float forwardAcceleration = 3.0f;
	[SerializeField] private float rotationSpeed = 3.0f;
	[SerializeField] private float topSpeed = 5.0f;
	[SerializeField, Range(0, 180)] private float visionAngle = 20.0f;
	private Rigidbody2D _myRigidBody;

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		//todo improve physics movement of chaser
		
		//handle moving forward of chaser
		Vector2 diff = transform.position - GameManager.Instance.Player.transform.position;
		if (Mathf.Abs(180.0f - Vector3.Angle(transform.up, diff)) < visionAngle)
		{
			_myRigidBody.velocity = (Vector3) transform.up * forwardAcceleration;
		}
		
		if (_myRigidBody.velocity.magnitude > topSpeed)
		{
			_myRigidBody.velocity = _myRigidBody.velocity.normalized * topSpeed;
		}

		//handle the rotation
		Quaternion targetRot = Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90);
		transform.rotation =
			Quaternion.RotateTowards(transform.rotation, targetRot,
				rotationSpeed * Time.deltaTime);
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.Instance.Player.Die();
		}
	}
}