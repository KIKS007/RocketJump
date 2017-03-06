using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChunksManager : MonoBehaviour
{
	[Header ("Previous Chunks")]
	public int SameTypeThreshold = 3;

	private int _sameTypeCount = 0;

	[Header ("Chunks List")]
	public Transform ChunksParent;
	public List<Chunk> AllChunks = new List<Chunk> ();
	public List<ChunkList> SortedChunks = new List<ChunkList> ();

	[Header ("Settings")]
	public int ChunkIndex = 1;
	public int AheadChunksCount = 3;

	[Header ("Lanes Parents")]
	public Transform LanesParent;

	private float _chunkHeight = 28f;
	private Transform _camera;

	private List<GameObject> _previousChunks = new List<GameObject> ();

	// Use this for initialization
	void Start () 
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		AllChunks.Clear ();

		foreach (Transform child in ChunksParent)
			AllChunks.Add (child.GetComponent<Chunk> ());

		AddFirstChunks ();

		SortChunks ();

		if (GameManager.Instance._initialState == GameState.Testing)
			TestingChunks ();
	}

	void SortChunks ()
	{
		foreach (Chunk chunk in AllChunks)
			SortedChunks [chunk.Difficulty].List.Add (chunk);
	}

	void AddFirstChunks ()
	{
		if(LanesParent.childCount > 0)
			_previousChunks.Add (LanesParent.GetChild (0).gameObject);
	}

	void TestingChunks ()
	{
		for (int i = 0; i < ChunksParent.childCount; i++)
		{
			ChunksParent.GetChild (i).transform.position = new Vector3 (0, _chunkHeight * (i + 1), 0);			
			ChunksParent.GetChild (i).gameObject.SetActive (true);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance._initialState == GameState.Playing)
		{
			if(_camera == null)
				_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
			
			if (_camera.position.y + (_chunkHeight * (AheadChunksCount - 1)) > _chunkHeight * ChunkIndex)
				AddNewChunks ();
		}
	}

	public void AddNewChunks ()
	{
		ChunkIndex++;

		RemovePreviousChunks ();
	}

	void AddNewChunk ()
	{
		
	}

	void AddRightOpenedLane ()
	{
		/*List<Chunk> firstLaneChunks = new List<Chunk> ();
		List<Chunk> secondLaneChunks = new List<Chunk> ();
		List<Chunk> thirdLaneChunks = new List<Chunk> ();
		GameObject chunk = null;

		//FIRST LANE
		firstLaneChunks.Clear ();
		firstLaneChunks = new List<Chunk> (_bothBreakable);
		firstLaneChunks.AddRange (_rightBreakable);
		firstLaneChunks.AddRange (_leftBreakable);
		firstLaneChunks.AddRange (_bothSolid);

		chunk = Instantiate (firstLaneChunks [Random.Range (0, firstLaneChunks.Count)].gameObject, new Vector3 (LaneChange.LanesPositions.x, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject;
		chunk.GetComponent<Chunk> ().ChunkPosition = LanePosition.First;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		//SECOND LANE
		secondLaneChunks.Clear ();
		secondLaneChunks = new List<Chunk> (_bothBreakable);
		secondLaneChunks.AddRange (_rightBreakable);

		chunk = Instantiate (secondLaneChunks [Random.Range (0, secondLaneChunks.Count)].gameObject, new Vector3 (LaneChange.LanesPositions.y, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [1]) as GameObject;
		chunk.GetComponent<Chunk> ().ChunkPosition = LanePosition.Second;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().RightBreakableBlocs));
		chunk.GetComponent<Chunk> ().EnableRightMeshes (true);

		//THIRD LANE
		thirdLaneChunks.Clear ();
		thirdLaneChunks = new List<Chunk> (_bothBreakable);
		thirdLaneChunks.AddRange (_leftBreakable);

		chunk = Instantiate (thirdLaneChunks [Random.Range (0, thirdLaneChunks.Count)].gameObject, new Vector3 (LaneChange.LanesPositions.z, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject;
		chunk.GetComponent<Chunk> ().ChunkPosition = LanePosition.Third;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().LeftBreakableBlocs));
		chunk.GetComponent<Chunk> ().EnableLeftMeshes (true);*/
	}		
		
	void RemovePreviousChunks ()
	{
		if (_previousChunks [0].transform.position.y < _camera.position.y - _chunkHeight * 1)
		{
			Destroy (_previousChunks [0]);
			Destroy (_previousChunks [1]);
			Destroy (_previousChunks [2]);

			for(int i = 0; i < 3; i++)
				_previousChunks.RemoveAt (0);
		}
	}
}

[Serializable]
public class ChunkList
{
	public List<Chunk> List;
}