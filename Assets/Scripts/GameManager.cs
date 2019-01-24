using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	private PlayerController player;
	public PlayerController Player => player;

	private CameraManager cameraManager;

	public CameraManager CameraManager => cameraManager;

	private List<GameObject> beacons = new List<GameObject>();
	public List<GameObject> Beacons => beacons;

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoadingScene;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoadingScene;
	}

	//this function is activated every time a scene is loaded
	private void OnLevelFinishedLoadingScene(Scene scene, LoadSceneMode mode)
	{
		Setup();
	}

	private void Setup()
	{
		player = FindObjectOfType<PlayerController>();
		foreach (var beacon in FindObjectsOfType<Beacon>())
		{
			beacons.Add(beacon.gameObject);
		}

		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void Awake()
	{
		cameraManager = GetComponent<CameraManager>();
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		Setup();
	}

	public void LoadLevel(string nameLevel)
	{
		SceneManager.LoadScene(nameLevel);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
}