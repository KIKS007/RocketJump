using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chunk : MonoBehaviour 
{
	[Header ("Difficulty")]
	[Range (0, 4)]
	public int Difficulty;

	[Header ("Meshes")]
	public List<GameObject> CompleteRightMeshes = new List<GameObject>();
	public List<GameObject> CompleteLeftMeshes = new List<GameObject>();
	public List<GameObject> BrokenRightMeshes = new List<GameObject>();
	public List<GameObject> BrokenLeftMeshes = new List<GameObject>();

    public List<GameObject> SpawnablePlatforms = new List<GameObject>();


    void Awake ()
	{
		FindObjectwithTag("SpawnablePlatform", SpawnablePlatforms);

		SpawnEnemies();

		SetupMeshes ();
	}

	void SetupMeshes ()
	{
		DisableAllMeshes ();
		
		EnableLeftMeshes (false);
		EnableRightMeshes (false);
	}

	void DisableAllMeshes ()
	{
		foreach (GameObject child in CompleteRightMeshes)
			child.SetActive (false);

		foreach (GameObject child in CompleteLeftMeshes)
			child.SetActive (false);

		foreach (GameObject child in BrokenRightMeshes)
			child.SetActive (false);

		foreach (GameObject child in BrokenLeftMeshes)
			child.SetActive (false);
	}

	void FindObjectwithTag(string _tag, List<GameObject> list, Predicate<GameObject> predicate = null)
	{
		list.Clear();
		Transform parent = transform;
		GetChildObject(parent, _tag, list, predicate);
	}

	void GetChildObject(Transform parent, string _tag, List<GameObject> list, Predicate<GameObject> predicate = null)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.tag == _tag)
			{
				if(predicate != null && predicate (child.gameObject))
					list.Add(child.gameObject);

				if(predicate == null)
					list.Add(child.gameObject);
			}
			if (child.childCount > 0)
			{
				GetChildObject(child, _tag, list, predicate);
			}
		}
	}

    void SpawnEnemies()
    {
		if (GameManager.Instance.GameState != GameState.Playing)
			return;
		
		int numberOfEnemies = EnemiesManager.Instance.numberOfEnemies;
		
		List<GameObject> SpawnablePlatformsTemp = new List<GameObject>(SpawnablePlatforms);

		for(int i = 0; i < numberOfEnemies; i++)
		{
			if (SpawnablePlatformsTemp.Count > 0)
			{
				
				GameObject enemy = EnemiesManager.Instance.Enemies[UnityEngine.Random.Range(0, EnemiesManager.Instance.Enemies.Count)];
				GameObject platform = SpawnablePlatformsTemp[UnityEngine.Random.Range(0, SpawnablePlatformsTemp.Count)];
				Vector3 position = SpawnablePlatformsTemp[UnityEngine.Random.Range(0, SpawnablePlatformsTemp.Count)].transform.position;
				position.y = position.y + (platform.transform.localScale.y / 2) + 1;
				
				Instantiate(enemy, position, enemy.transform.rotation, EnemiesManager.Instance.enemiesParent);
				
				
				SpawnablePlatformsTemp.Remove(platform);
			}
		}

        //numberOfEnemies
    }

	public void EnableRightMeshes (bool opened)
	{
		foreach (GameObject child in CompleteRightMeshes)
			child.SetActive (false);

		foreach (GameObject child in BrokenRightMeshes)
			child.SetActive (false);

		if(opened)
			BrokenRightMeshes [UnityEngine.Random.Range (0, BrokenRightMeshes.Count)].SetActive (true);
		else
			CompleteRightMeshes [UnityEngine.Random.Range (0, CompleteRightMeshes.Count)].SetActive (true);
	}

	public void EnableLeftMeshes (bool opened)
	{
		foreach (GameObject child in CompleteLeftMeshes)
			child.SetActive (false);

		foreach (GameObject child in BrokenLeftMeshes)
			child.SetActive (false);

		if(opened)
			BrokenLeftMeshes [UnityEngine.Random.Range (0, BrokenLeftMeshes.Count)].SetActive (true);
		else
			CompleteLeftMeshes [UnityEngine.Random.Range (0, CompleteLeftMeshes.Count)].SetActive (true);
	}
}
