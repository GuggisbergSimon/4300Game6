using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float verticalSpeed = 5.0f;
	[SerializeField] private float rotationSpeed = 5.0f;
	[SerializeField] private float maxForwardSpeed = 10.0f;
	[SerializeField] private float respawnSpeed = 2.0f;
	[SerializeField] private float invincibilityTime = 1.0f;
	[SerializeField] private float amplitudeGainRespawn = 5.0f;
	[SerializeField] private float frequencyGainRespawn = 2.0f;
	private float _inputHorizontal;
	private float _inputVertical;
	private Rigidbody2D _myRigidBody;
	private bool _isAlive = true;
	private bool isInvincible = false;
	public bool IsInvincible => isInvincible;
	private Vector3 respawnPos;
	private List<GameObject> _beaconsActivated = new List<GameObject>();

	private void Start()
	{
		respawnPos = transform.position;
		_myRigidBody = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		//todo improve physics of player

		//handles the move forward of the player
		if (_inputVertical > 0)
		{
			_myRigidBody.velocity += (Vector2) transform.up * _inputVertical * verticalSpeed;
		}

		if (_myRigidBody.velocity.magnitude > maxForwardSpeed)
		{
			_myRigidBody.velocity = _myRigidBody.velocity.normalized * maxForwardSpeed;
		}

		//handles the rotation of the player
		_myRigidBody.MoveRotation(transform.rotation.eulerAngles.z - rotationSpeed * _inputHorizontal);
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
		if (_beaconsActivated.Count >= GameManager.Instance.Beacons.Count)
		{
			//todo implement end of game here
		}
	}

	private IEnumerator Respawn(float speed)
	{
		GameManager.Instance.CameraManager.Noise(amplitudeGainRespawn, frequencyGainRespawn);
		float timer = 0.0f;
		Vector3 initPos = transform.position;
		float time = (initPos - respawnPos).magnitude/speed;
		while (timer < time)
		{
			transform.position = Vector3.Lerp(initPos, respawnPos, timer / time);
			timer += Time.deltaTime;
			yield return null;
		}

		isInvincible = true;
		_isAlive = true;
		GameManager.Instance.CameraManager.Noise(0, 0);
		yield return new WaitForSeconds(invincibilityTime);
		isInvincible = false;
	}

	public void Die()
	{
		if (_isAlive)
		{
			_inputHorizontal = 0.0f;
			_inputVertical = 0.0f;
			_isAlive = false;
			StartCoroutine(Respawn(respawnSpeed));
			//todo implement code to fade to black
		}
	}
}