using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunksManager : Singleton<ChunksManager> 
{
	[Header ("Chunks")]
	public List<Chunk> FirstLaneChuncks = new List<Chunk> ();
	public List<Chunk> SecondLaneChuncks = new List<Chunk> ();
	public List<Chunk> ThirdLaneChuncks = new List<Chunk> ();

	[Header ("Settings")]
	public int ChunkIndex = 1;
	public int AheadChunksCount = 3;

	[Header ("Lanes Parents")]
	public Transform[] LanesParents = new Transform[3];

	private float _chunkHeight = 28f;
	private Transform _camera;

	private List<Chunk> _firstLaneOpened = new List<Chunk> ();
	private List<Chunk> _firstLaneClosed = new List<Chunk> ();

	private List<Chunk> _thirdLaneOpened = new List<Chunk> ();
	private List<Chunk> _thirdLaneClosed = new List<Chunk> ();

	private List<GameObject> _previousChunks = new List<GameObject> ();

	// Use this for initialization
	void Start () 
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		AddFirstChunks ();

		foreach (Chunk chunk in FirstLaneChuncks)
		{
			if (chunk.RightWall != WallType.Solid)
				_firstLaneOpened.Add (chunk);
			else
				_firstLaneClosed.Add (chunk);
		}

		foreach (Chunk chunk in ThirdLaneChuncks)
		{
			if (chunk.LeftWall != WallType.Solid)
				_thirdLaneOpened.Add (chunk);
			else
				_thirdLaneClosed.Add (chunk);
		}
	}

	void AddFirstChunks ()
	{
		if(LanesParents [0].childCount > 0)
			_previousChunks.Add (LanesParents [0].GetChild (0).gameObject);

		if(LanesParents [1].childCount > 0)
			_previousChunks.Add (LanesParents [1].GetChild (0).gameObject);

		if(LanesParents [2].childCount > 0)
			_previousChunks.Add (LanesParents [2].GetChild (0).gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_camera == null)
			_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		if (_camera.position.y + (_chunkHeight * (AheadChunksCount - 1)) > _chunkHeight * ChunkIndex)
			AddChunks ();
	}

	public void AddChunks ()
	{
		ChunkIndex++;

		Chunk secondLane = SecondLaneChuncks [Random.Range (0, SecondLaneChuncks.Count)];
		Chunk firstlane = null;
		Chunk thirdlane = null;

		GameObject secondChunk = Instantiate (secondLane.gameObject, new Vector3 (0, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [1]) as GameObject;
		_previousChunks.Add (secondChunk);

		//First Lane
		if(secondLane.LeftWall == WallType.Opened)
		{
			firstlane = _firstLaneOpened [Random.Range (0, _firstLaneOpened.Count)];

			GameObject chunk = Instantiate (firstlane.gameObject, new Vector3 (0, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject;
			_previousChunks.Add (chunk);

			StartCoroutine (RemoveBlocs (secondChunk.GetComponent<Chunk> ().LeftBreakableBlocs));
			StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().RightBreakableBlocs));
		}
		else
		{
			firstlane = _firstLaneClosed [Random.Range (0, _firstLaneClosed.Count)];

			_previousChunks.Add (Instantiate (firstlane.gameObject, new Vector3 (0, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject);
		}

		//Third Lane
		if(secondLane.RightWall == WallType.Opened)
		{
			thirdlane = _thirdLaneOpened [Random.Range (0, _thirdLaneOpened.Count)];

			GameObject chunk = Instantiate (thirdlane.gameObject, new Vector3 (0, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject;
			_previousChunks.Add (chunk);

			StartCoroutine (RemoveBlocs (secondChunk.GetComponent<Chunk> ().RightBreakableBlocs));
			StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().LeftBreakableBlocs));
		}
		else
		{
			thirdlane = _thirdLaneClosed [Random.Range (0, _thirdLaneClosed.Count)];

			_previousChunks.Add (Instantiate (thirdlane.gameObject, new Vector3 (0, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject);
		}


		for (int i = 1; i < 4; i++)
			_previousChunks [_previousChunks.Count - i].SetActive (true);

		RemovePreviousChunks ();
	}

	IEnumerator RemoveBlocs (List<GameObject> blocs)
	{
		yield return new WaitForSeconds (0.05f);

		foreach (GameObject bloc in blocs)
			Destroy (bloc);
	}

	void RemovePreviousChunks ()
	{
		if (_previousChunks [0].transform.position.y < _camera.position.y - _chunkHeight * 2)
		{
			Destroy (_previousChunks [0]);
			Destroy (_previousChunks [1]);
			Destroy (_previousChunks [2]);

			for(int i = 0; i < 3; i++)
				_previousChunks.RemoveAt (0);
		}
	}
}
