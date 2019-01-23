using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speedVertical = 5.0f;
	[SerializeField] private float speedRotation = 5.0f;
	[SerializeField] private float speedMaxForward = 10.0f;
	[SerializeField] private float speedMaxBackward = 2.5f;
	private float _inputHorizontal;
	private float _inputVertical;
	private Rigidbody2D _myRigidBody;

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		//todo improve physics of player
		
		//handles the move forward of the player
		_myRigidBody.velocity += (Vector2) transform.up * _inputVertical * speedVertical;
		if (_myRigidBody.velocity.magnitude > speedMaxForward)
		{
			_myRigidBody.velocity = _myRigidBody.velocity.normalized *
			                        (_inputVertical > 0 ? speedMaxForward : speedMaxBackward);
		}

		//handles the rotation of the player
		_myRigidBody.MoveRotation(transform.rotation.eulerAngles.z - speedRotation * _inputHorizontal);
	}

	private void Update()
	{
		_inputHorizontal = Input.GetAxis("Horizontal");
		_inputVertical = Input.GetAxis("Vertical");
	}

	public void Die()
	{
		//todo implement dying method.
	}
}