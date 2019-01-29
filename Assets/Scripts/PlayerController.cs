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
		_respawnPos = transform.position;
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
		GameManager.Instance.BeaconsActivated.Add(beacon);
		for (int i = 0; i < GameManager.Instance.BeaconsLeft.Count; i++)
		{
			if (beacon.transform.position == GameManager.Instance.BeaconsLeft[i].transform.position)
			{
				GameManager.Instance.BeaconsLeft.RemoveAt(i);
			}
		}
		GameManager.Instance.BeaconFinder.ReScan();

		if (GameManager.Instance.BeaconsActivated.Count >= GameManager.Instance.Beacons.Count)
		{
			GameManager.Instance.EndGame();
		}
	}

	private IEnumerator Respawn()
	{
		GameManager.Instance.CameraManager.Noise(amplitudeGainRespawn, frequencyGainRespawn);
		GameManager.Instance.UiManager.ToggleVignette(true);
		float timer = 0.0f;
		Vector3 initPos = transform.position;
		float time = (initPos - _respawnPos).magnitude / respawnSpeed;
		while (timer < time)
		{
			transform.position = Vector3.Lerp(initPos, _respawnPos, timer / time);
			timer += Time.deltaTime;
			yield return null;
		}

		_isInvincible = true;
		_isAlive = true;
		GameManager.Instance.CameraManager.Noise(0, 0);
		GameManager.Instance.UiManager.ToggleVignette(false);
		yield return new WaitForSeconds(invincibilityTime);
		_isInvincible = false;
	}

	public void Die()
	{
		if (_isAlive)
		{
			_inputHorizontal = 0.0f;
			_inputVertical = 0.0f;
			_isAlive = false;
			StartCoroutine(Respawn());
		}
	}
}