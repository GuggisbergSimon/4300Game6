using UnityEngine;

public class Chaser : MonoBehaviour
{
	[SerializeField] private float speedForward = 3.0f;
	[SerializeField] private float speedRotation = 3.0f;
	private Rigidbody2D _myRigidBody;

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		//todo improve physics of chaser
		//handle the velocity
		_myRigidBody.velocity = (Vector3) transform.up * speedForward;
		
		//handle the rotation
		Vector2 diff = transform.position - GameManager.Instance.Player.transform.position;
		Quaternion targetRot = Quaternion.Euler(0f, 0f, Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90);
		transform.rotation =
			Quaternion.RotateTowards(transform.rotation, targetRot, speedRotation * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			GameManager.Instance.Player.Die();
		}
	}
}