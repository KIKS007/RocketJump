using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackgroundClouds : MonoBehaviour 
{
	public GameObject[] AllClouds = new GameObject[0];
	public GameObject[] Backgrounds = new GameObject[3];
	public Transform CloudsParent;

	[Header ("Settings")]
	public Vector2 RandomScale;
	public Vector2 RandomMovementDuration;
	public Vector2 XMovementPositions;
	public Vector2 RandomYPosition;
	public Vector2 RandomDelay;

	private Transform _mainCamera;

	// Use this for initialization
	void Start () 
	{
		_mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		GameManager.Instance.OnGameOver += () => StopAllCoroutines ();
		GameManager.Instance.OnPlaying += () => StartCoroutine (SpawnCloud ());
	}

	IEnumerator SpawnCloud ()
	{
		GameObject cloud = Instantiate (AllClouds [Random.Range (0, AllClouds.Length)], transform.transform.position, Quaternion.identity, CloudsParent) as GameObject;

		cloud.transform.localRotation = Quaternion.Euler (new Vector3 (0, 180, 0));
		cloud.transform.position = new Vector3 (XMovementPositions.y, _mainCamera.position.y + Random.Range (RandomYPosition.x, RandomYPosition.y), Backgrounds [Random.Range (0, Backgrounds.Length)].transform.position.z - 0.05f);

		float randomScale = Random.Range (RandomScale.x, RandomScale.y);
		cloud.transform.localScale = new Vector3 (randomScale, randomScale, 1);

		cloud.transform.DOMoveX (XMovementPositions.x, Random.Range (RandomMovementDuration.x, RandomMovementDuration.y)).OnComplete (()=> Destroy (cloud.gameObject));

		yield return new WaitForSeconds (Random.Range (RandomDelay.x, RandomDelay.y));

		StartCoroutine (SpawnCloud ());
	}
}
