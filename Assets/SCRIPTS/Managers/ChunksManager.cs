using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChunksManager : MonoBehaviour
{
	[Header ("Difficulty")]
	public List<int> PreviousDifficultyLevels = new List<int>();
	public int CurrentDifficulty = 0;
	public int DifficultyThreshold = 200;
	public AnimationCurve DifficultyCurve;

	[Header ("Settings")]
	public int ChunkIndex = 1;
	public int AheadChunksCount = 1;
	public float ChunkHeight = 28f;

	[Header ("Chunks List")]
	public Transform ChunksPrefabs;
	public List<Chunk> AllChunks = new List<Chunk> ();
	public List<ChunkList> SortedChunks = new List<ChunkList> ();

	[Header ("Previous Chunks List")]
	public List<GameObject> PreviousChunksSpawned = new List<GameObject> ();
	public List<Chunk> PreviousChunks = new List<Chunk> ();

	[Header ("Testing")]
	public bool OnlyActiveOnes = false;
	public bool SortByDifficulty = false;

	[Header ("Lanes Parents")]
	public Transform ChunksParent;

	private Transform _camera;

	// Use this for initialization
	void Start () 
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		AllChunks.Clear ();

		if (GameManager.Instance._initialState != GameState.Testing || !OnlyActiveOnes)
			foreach (Transform child in ChunksPrefabs)
				AllChunks.Add (child.GetComponent<Chunk> ());
		else
			foreach (Transform child in ChunksPrefabs)
				if(child.gameObject.activeSelf == true)
					AllChunks.Add (child.GetComponent<Chunk> ());
			
		AddFirstChunks ();

		SortChunks ();

		if (GameManager.Instance._initialState == GameState.Testing)
			TestingChunks ();
	}

	void SortChunks ()
	{
		SortedChunks.Clear ();

		for (int i = 0; i < 5; i++)
		{
			SortedChunks.Add (new ChunkList ());
			SortedChunks [i].List = new List<Chunk> ();
		}

		foreach (Chunk chunk in AllChunks)
			SortedChunks [chunk.Difficulty].List.Add (chunk);
	}

	void AddFirstChunks ()
	{
		int randomFirstStart = UnityEngine.Random.Range (0, ChunksParent.childCount);

		for(int i = 0; i < ChunksParent.childCount; i++)
			if(i != randomFirstStart)
				ChunksParent.GetChild (i).gameObject.SetActive (false);
			else
				ChunksParent.GetChild (i).gameObject.SetActive (true);

		if(ChunksParent.childCount > 0)
			PreviousChunksSpawned.Add (ChunksParent.GetChild (randomFirstStart).gameObject);
	}

	void TestingChunks ()
	{
		if(!SortByDifficulty)
		{
			if(!OnlyActiveOnes)
			{
				for (int i = 0; i < ChunksPrefabs.childCount; i++)
				{
					ChunksPrefabs.GetChild (i).transform.position = new Vector3 (0, ChunkHeight * (i + 1), 0);	
					ChunksPrefabs.GetChild (i).gameObject.SetActive (true);
				}
			}
			else
			{
				for(int i = 0; i < AllChunks.Count; i++)
				{
					AllChunks [i].transform.position = new Vector3 (0, ChunkHeight * (i + 1), 0);
					AllChunks [i].gameObject.SetActive (true);
				}
			}
		}
		
		else
		{
			int index = 0;
			
			foreach(ChunkList chunkList in SortedChunks)
			{
				foreach(Chunk chunk in chunkList.List)
				{
					chunk.transform.position = new Vector3 (0, ChunkHeight * (index + 1), 0);
					
					if(!OnlyActiveOnes)
						chunk.gameObject.SetActive (true);
					
					index++;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance._initialState == GameState.Playing || GameManager.Instance._initialState == GameState.Menu)
		{
			if(_camera == null)
				_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
			
			if (_camera.position.y + (ChunkHeight * (AheadChunksCount - 1)) > ChunkHeight * ChunkIndex)
				AddNewChunk ();

			if ((CurrentDifficulty + 1) * DifficultyThreshold < ScoreManager.Instance.ClimbingScore && CurrentDifficulty < 4)
				CurrentDifficulty++;
		}
	}

	public void AddNewChunk ()
	{
		ChunkIndex++;

		Chunk newChunk = null;
		GameObject chunkSpawned = null;

		float evaluatedDifficulty = DifficultyCurve.Evaluate ((float)(ScoreManager.Instance.ClimbingScore - CurrentDifficulty * DifficultyThreshold) / DifficultyThreshold);

		int difficultyIndex = CurrentDifficulty;

		if (evaluatedDifficulty > -0.05f && evaluatedDifficulty < 0.05f)
			difficultyIndex = CurrentDifficulty;
		
		else if(evaluatedDifficulty > 0.05f && CurrentDifficulty < 4)
			difficultyIndex = CurrentDifficulty + 1;

		else if(evaluatedDifficulty < -0.05f && CurrentDifficulty > 0)
			difficultyIndex = CurrentDifficulty - 1;

		/*Debug.Log ("Evaluated Difficulty : " + evaluatedDifficulty);
		Debug.Log ("CurrentDifficulty : " + CurrentDifficulty);
		Debug.Log ("Index : " + difficultyIndex);*/

		PreviousDifficultyLevels.Add (difficultyIndex);
		bool validChunk = true;

		do
		{
			validChunk = true;

			if(SortedChunks [difficultyIndex].List.Count == 0)
			{
				Debug.LogWarning ("There's no Chunks!");
				difficultyIndex = 0;
			}

			else if(SortedChunks [difficultyIndex].List.Count < 4)
			{
				Debug.LogWarning ("Not Enough Chunks!");
				difficultyIndex = 0;
			}


			newChunk = SortedChunks [difficultyIndex].List [UnityEngine.Random.Range (0, SortedChunks [difficultyIndex].List.Count)];
			
			if(PreviousChunksSpawned.Count > 0 && PreviousChunksSpawned [0] == newChunk)
				validChunk = false;
			
		}
		while (!validChunk);


		chunkSpawned = Instantiate (newChunk.gameObject, new Vector3 (0, ChunkHeight * ChunkIndex, 0), Quaternion.identity, ChunksParent) as GameObject;

		PreviousChunksSpawned.Insert (0, chunkSpawned);
		PreviousChunks.Insert (0, newChunk);

		chunkSpawned.SetActive (true);

		newChunk.EnableRightMeshes (false);
		newChunk.EnableLeftMeshes (false);

		CollectiblesManager.Instance.SpawnCollectibles (chunkSpawned);
        FlyingMobManager.Instance.SpawnCollectibles (chunkSpawned);

		RemovePreviousChunks ();
	}		
		
	void RemovePreviousChunks ()
	{
		if (PreviousChunksSpawned [PreviousChunksSpawned.Count - 1] != null && PreviousChunksSpawned [PreviousChunksSpawned.Count - 1].transform.position.y < _camera.position.y - ChunkHeight * 1)
		{
			Destroy (PreviousChunksSpawned [PreviousChunksSpawned.Count - 1]);

			PreviousChunksSpawned.RemoveAt (PreviousChunksSpawned.Count - 1);
		}
	}

	#if UNITY_EDITOR
	[ContextMenu ("Revert LD Prefabs")]
	public void RevertPrefabs ()
	{
		foreach(Transform chunk in ChunksPrefabs)
		{
			Transform parent = chunk.Find ("Level Design");

			foreach (Transform child in parent)
				PrefabUtility.RevertPrefabInstance (child.gameObject);
		}
	}
	#endif
}

[Serializable]
public class ChunkList
{
	public List<Chunk> List;
}