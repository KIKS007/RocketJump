using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChunkType { RightOpened, LeftOpened, BothOpened, BothClosed };

public class ChunksManager : Singleton<ChunksManager> 
{
	[Header ("Previous Chunks")]
	public List<ChunkType> PreviousChunksType = new List<ChunkType> ();
	public int SameTypeThreshold = 3;

	[Header ("Chunks Chance")]
	public int RightOpenedChance = 1;
	public int LeftOpenedChance = 1;
	public int BothOpenedChance = 1;
	public int BothClosedChance = 1;

	private int _sameTypeCount = 0;

	[Header ("Chunks List")]
	public Transform ChunksParent;
	public List<Chunk> AllChunks = new List<Chunk> ();

	[Header ("Settings")]
	public int ChunkIndex = 1;
	public int AheadChunksCount = 3;

	[Header ("Lanes Parents")]
	public Transform[] LanesParents = new Transform[3];

	private float _chunkHeight = 28f;
	private Transform _camera;

	private List<Chunk> _bothBreakable = new List<Chunk> ();
	private List<Chunk> _rightBreakable = new List<Chunk> ();
	private List<Chunk> _leftBreakable = new List<Chunk> ();
	private List<Chunk> _bothSolid = new List<Chunk> ();

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
	}

	void SortChunks ()
	{
		foreach (Chunk chunk in AllChunks)
		{
			chunk.gameObject.SetActive (false);

			if(chunk.RightWall == WallType.Breakable)
			{
				//Both breakable
				if (chunk.LeftWall == WallType.Breakable)
					_bothBreakable.Add (chunk);

				//Right breakable
				else if(chunk.LeftWall == WallType.Solid)
					_rightBreakable.Add (chunk);
			}

			else if(chunk.LeftWall == WallType.Breakable)
			{
				//Both breakable
				if (chunk.RightWall == WallType.Breakable)
					_bothBreakable.Add (chunk);

				//Left breakable
				else if(chunk.RightWall == WallType.Solid)
					_leftBreakable.Add (chunk);
			}

			//Both solid
			else if(chunk.RightWall == WallType.Solid && chunk.LeftWall == WallType.Solid)
				_bothSolid.Add (chunk);
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
			AddNewChunks ();
	}

	public void AddNewChunks ()
	{
		ChunkIndex++;

		ChunkType nextChunkType = RandomChunkType ();
		PreviousChunksType.Insert (0, nextChunkType);

		if (PreviousChunksType.Count > 10)
			PreviousChunksType.RemoveAt (PreviousChunksType.Count - 1);

		switch(nextChunkType)
		{
		case ChunkType.RightOpened:
			AddRightOpenedLane ();
			break;
		case ChunkType.LeftOpened:
			AddLeftOpenedLane ();
			break;
		case ChunkType.BothOpened:
			AddBothOpenedLane ();
			break;
		case ChunkType.BothClosed:
			AddBothClosedLane ();
			break;
		}

		RemovePreviousChunks ();
	}

	void AddRightOpenedLane ()
	{
		List<Chunk> firstLaneChunks = new List<Chunk> ();
		List<Chunk> secondLaneChunks = new List<Chunk> ();
		List<Chunk> thirdLaneChunks = new List<Chunk> ();
		GameObject chunk = null;

		//FIRST LANE
		firstLaneChunks.Clear ();
		firstLaneChunks = new List<Chunk> (_bothBreakable);
		firstLaneChunks.AddRange (_rightBreakable);
		firstLaneChunks.AddRange (_leftBreakable);
		firstLaneChunks.AddRange (_bothSolid);

		chunk = Instantiate (firstLaneChunks [Random.Range (0, firstLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.x, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		//SECOND LANE
		secondLaneChunks.Clear ();
		secondLaneChunks = new List<Chunk> (_bothBreakable);
		secondLaneChunks.AddRange (_rightBreakable);

		chunk = Instantiate (secondLaneChunks [Random.Range (0, secondLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.y, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [1]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().RightBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.RightOpened));

		//THIRD LANE
		thirdLaneChunks.Clear ();
		thirdLaneChunks = new List<Chunk> (_bothBreakable);
		thirdLaneChunks.AddRange (_leftBreakable);

		chunk = Instantiate (thirdLaneChunks [Random.Range (0, thirdLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.z, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().LeftBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.LeftOpened));
	}

	void AddLeftOpenedLane ()
	{
		List<Chunk> firstLaneChunks = new List<Chunk> ();
		List<Chunk> secondLaneChunks = new List<Chunk> ();
		List<Chunk> thirdLaneChunks = new List<Chunk> ();
		GameObject chunk = null;

		//FIRST LANE
		firstLaneChunks.Clear ();
		firstLaneChunks = new List<Chunk> (_bothBreakable);
		firstLaneChunks.AddRange (_rightBreakable);

		chunk = Instantiate (firstLaneChunks [Random.Range (0, firstLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.x, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().RightBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.RightOpened));

		//SECOND LANE
		secondLaneChunks.Clear ();
		secondLaneChunks = new List<Chunk> (_bothBreakable);
		secondLaneChunks.AddRange (_leftBreakable);

		chunk = Instantiate (secondLaneChunks [Random.Range (0, secondLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.y, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [1]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().LeftBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.LeftOpened));

		//THIRD LANE
		thirdLaneChunks.Clear ();
		thirdLaneChunks = new List<Chunk> (_bothBreakable);
		thirdLaneChunks.AddRange (_leftBreakable);

		chunk = Instantiate (thirdLaneChunks [Random.Range (0, thirdLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.z, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);
	}

	void AddBothOpenedLane ()
	{
		List<Chunk> firstLaneChunks = new List<Chunk> ();
		List<Chunk> secondLaneChunks = new List<Chunk> ();
		List<Chunk> thirdLaneChunks = new List<Chunk> ();
		GameObject chunk = null;

		//FIRST LANE
		firstLaneChunks.Clear ();
		firstLaneChunks = new List<Chunk> (_bothBreakable);
		firstLaneChunks.AddRange (_rightBreakable);

		chunk = Instantiate (firstLaneChunks [Random.Range (0, firstLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.x, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().RightBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.RightOpened));

		//SECOND LANE
		secondLaneChunks.Clear ();
		secondLaneChunks = new List<Chunk> (_bothBreakable);

		chunk = Instantiate (secondLaneChunks [Random.Range (0, secondLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.y, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [1]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().LeftBreakableBlocs));
		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().RightBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.LeftOpened));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.RightOpened));

		//THIRD LANE
		thirdLaneChunks.Clear ();
		thirdLaneChunks = new List<Chunk> (_bothBreakable);
		thirdLaneChunks.AddRange (_leftBreakable);

		chunk = Instantiate (thirdLaneChunks [Random.Range (0, thirdLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.z, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		StartCoroutine (RemoveBlocs (chunk.GetComponent<Chunk> ().LeftBreakableBlocs));
		StartCoroutine (EnableLaneChanges (chunk.GetComponent<Chunk> (), ChunkType.LeftOpened));
	}

	void AddBothClosedLane ()
	{
		List<Chunk> firstLaneChunks = new List<Chunk> ();
		List<Chunk> secondLaneChunks = new List<Chunk> ();
		List<Chunk> thirdLaneChunks = new List<Chunk> ();
		GameObject chunk = null;

		//FIRST LANE
		firstLaneChunks.Clear ();
		firstLaneChunks = new List<Chunk> (_bothBreakable);
		firstLaneChunks.AddRange (_rightBreakable);
		firstLaneChunks.AddRange (_leftBreakable);
		firstLaneChunks.AddRange (_bothSolid);

		chunk = Instantiate (firstLaneChunks [Random.Range (0, firstLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.x, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [0]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		//SECOND LANE
		secondLaneChunks.Clear ();
		secondLaneChunks = new List<Chunk> (_bothBreakable);
		secondLaneChunks.AddRange (_rightBreakable);
		secondLaneChunks.AddRange (_leftBreakable);
		secondLaneChunks.AddRange (_bothSolid);

		chunk = Instantiate (secondLaneChunks [Random.Range (0, secondLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.y, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [1]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);

		//THIRD LANE
		thirdLaneChunks.Clear ();
		thirdLaneChunks = new List<Chunk> (_bothBreakable);
		thirdLaneChunks.AddRange (_rightBreakable);
		thirdLaneChunks.AddRange (_leftBreakable);
		thirdLaneChunks.AddRange (_bothSolid);

		chunk = Instantiate (thirdLaneChunks [Random.Range (0, thirdLaneChunks.Count)].gameObject, new Vector3 (LaneChange._lanesPositions.z, _chunkHeight * ChunkIndex, 0), Quaternion.identity, LanesParents [2]) as GameObject;
		chunk.SetActive (true);
		_previousChunks.Add (chunk);
	}

	ChunkType RandomChunkType ()
	{
		List<ChunkType> chunksType = new List<ChunkType> ();

		for (int i = 0; i < RightOpenedChance; i++)
			chunksType.Add (ChunkType.RightOpened);

		for (int i = 0; i < LeftOpenedChance; i++)
			chunksType.Add (ChunkType.LeftOpened);

		for (int i = 0; i < BothOpenedChance; i++)
			chunksType.Add (ChunkType.BothOpened);

		for (int i = 0; i < BothClosedChance; i++)
			chunksType.Add (ChunkType.BothClosed);

		ChunkType chunkReturned = ChunkType.BothClosed;


		if(!CheckChances ())
			return chunkReturned;

		if(_sameTypeCount == SameTypeThreshold - 1)
		{
			do
			{
				chunkReturned = chunksType [Random.Range (0, chunksType.Count)];
			}
			while (PreviousChunksType [0] == chunkReturned);

			_sameTypeCount = 0;
		}
		else
		{
			chunkReturned = chunksType [Random.Range (0, chunksType.Count)];

			if (PreviousChunksType.Count > 0 && PreviousChunksType [0] == chunkReturned)
				_sameTypeCount++;
			else
				_sameTypeCount = 0;
		}


		return chunkReturned;
	}
		
	bool CheckChances ()
	{
		int noChanceCount = 0;

		if (RightOpenedChance == 0)
			noChanceCount++;

		if (LeftOpenedChance == 0)
			noChanceCount++;

		if (BothOpenedChance == 0)
			noChanceCount++;
		
		if (BothClosedChance == 0)
			noChanceCount++;


		if (noChanceCount > 2) {
			Debug.LogError ("There aren't enough chances!");
			return false;
		} else
			return true;
	}

	IEnumerator RemoveBlocs (List<GameObject> blocs)
	{
		yield return new WaitForSeconds (0.05f);

		foreach (GameObject bloc in blocs)
			Destroy (bloc);
	}

	IEnumerator EnableLaneChanges (Chunk chunk, ChunkType type)
	{
		yield return new WaitForSeconds (0.05f);

		if (type == ChunkType.BothOpened || type == ChunkType.RightOpened)
		{
			chunk._rightLaneChange.SetActive (true);

			chunk._leftLaneChange.SetActive (false);
		}

		if (type == ChunkType.BothOpened || type == ChunkType.LeftOpened)
		{
			chunk._leftLaneChange.SetActive (true);

			chunk._rightLaneChange.SetActive (false);
		}
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
