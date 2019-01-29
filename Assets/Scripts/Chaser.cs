using UnityEngine;

public class Chaser : MonoBehaviour
{
	[SerializeField] private float forwardAcceleration = 3.0f;
	[SerializeField] private float rotationSpeed = 3.0f;
	[SerializeField] private float topSpeed = 5.0f;
	[SerializeField, Range(0, 180)] private float visionAngle = 20.0f;
	private Rigidbody2D _myRigidBody;
	private ParticleSystem _myParticleSystem;
	private Collider2D _myCollider;

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
		_myParticleSystem = GetComponent<ParticleSystem>();
		_myCollider = GetComponent<Collider2D>();
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

	public void StopChasing()
	{
		_myParticleSystem.Stop();
		Destroy(_myCollider);
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.GetComponent<PlayerController>().IsInvincible)
		{
			GameManager.Instance.Player.Die();
		}
	}
}