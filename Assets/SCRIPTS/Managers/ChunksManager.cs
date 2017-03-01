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


	private float _chunkHeight = 28f;
	private Transform _camera;

	private List<Chunk> _firstLaneOpened;
	private List<Chunk> _firstLaneClosed;

	private List<Chunk> _secondLaneOpened;
	private List<Chunk> _secondLaneClosed;

	// Use this for initialization
	void Start () 
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_camera.position.y + (_chunkHeight * AheadChunksCount) > _chunkHeight * ChunkIndex)
			AddChunk ();
	}

	public void AddChunk ()
	{
		ChunkIndex++;

		Chunk secondLane = SecondLaneChuncks [Random.Range (0, SecondLaneChuncks.Count)];


	}
}
