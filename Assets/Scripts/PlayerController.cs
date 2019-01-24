using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speedVertical = 5.0f;
	[SerializeField] private float speedRotation = 5.0f;
	[SerializeField] private float speedMaxForward = 10.0f;
	private float _inputHorizontal;
	private float _inputVertical;
	private Rigidbody2D _myRigidBody;
	private bool _isAlive = true;
	private List<GameObject> _beaconsActivated = new List<GameObject>();

	private void Start()
	{
		_myRigidBody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		//todo improve physics of player

		//handles the move forward of the player
		if (_inputVertical > 0)
		{
			_myRigidBody.velocity += (Vector2) transform.up * _inputVertical * speedVertical;
		}

		if (_myRigidBody.velocity.magnitude > speedMaxForward)
		{
			_myRigidBody.velocity = _myRigidBody.velocity.normalized * speedMaxForward;
		}

		//handles the rotation of the player
		_myRigidBody.MoveRotation(transform.rotation.eulerAngles.z - speedRotation * _inputHorizontal);
	}

	private void Update()
	{
		if (_isAlive)
		{
			_inputHorizontal = Input.GetAxis("Horizontal");
			_inputVertical = Input.GetAxis("Vertical");
		}
	}

	public void BeaconActivated(GameObject beacon)
	{
		_beaconsActivated.Add(beacon);
	}

	private void BeaconsDeactivate()
	{
		if (_beaconsActivated.Count > 0)
		{
			_beaconsActivated[_beaconsActivated.Count-1].SetActive(true);
			_beaconsActivated.RemoveAt(-_beaconsActivated.Count - 1);
		}
	}
	
	public void Die()
	{
		if (_isAlive)
		{
			_inputHorizontal = 0.0f;
			_inputVertical = 0.0f;
			_isAlive = false;
			BeaconsDeactivate();
			//todo choose what dying represent in this game
		}
	}
}