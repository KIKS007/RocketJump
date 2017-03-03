using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WallType { Solid, Breakable };

public class Chunk : MonoBehaviour 
{
	[Header ("Wall Type")]
	public WallType RightWall;
	public WallType LeftWall;

	[Header ("Breakable Blocs")]
	public List<GameObject> RightBreakableBlocs = new List<GameObject>();
	public List<GameObject> LeftBreakableBlocs = new List<GameObject>();

	[Header ("Meshes")]
	public List<GameObject> CompleteRightMeshes = new List<GameObject>();
	public List<GameObject> CompleteLeftMeshes = new List<GameObject>();
	public List<GameObject> BrokenRightMeshes = new List<GameObject>();
	public List<GameObject> BrokenLeftMeshes = new List<GameObject>();

	[HideInInspector]
	public GameObject _rightLaneChange;
	[HideInInspector]
	public GameObject _leftLaneChange;


	void Awake ()
	{
		if(RightWall != WallType.Solid)
		{
			FindObjectwithTag ("BreakableBloc", RightBreakableBlocs, x => x.transform.position.x > transform.position.x);

			if(RightBreakableBlocs.Count == 0)
				Debug.LogWarning ("No Right Breakable Blocs! Check Tags : " + name);
		}

		if(LeftWall != WallType.Solid)
		{
			FindObjectwithTag ("BreakableBloc", LeftBreakableBlocs, x => x.transform.position.x < transform.position.x);

			if(LeftBreakableBlocs.Count == 0)
				Debug.LogWarning ("No Right Breakable Blocs! Check Tags : " + name);
		}

		LaneChange[] lanesChanges = transform.GetComponentsInChildren<LaneChange> ();
		
		if(lanesChanges.Length > 0)
		{
			foreach(LaneChange laneChange in lanesChanges)
			{
				if(laneChange.Change == LaneChange.ChangeType.Previous)
				{
					_leftLaneChange = laneChange.gameObject;
					_leftLaneChange.SetActive (false);
				}
				
				if(laneChange.Change == LaneChange.ChangeType.Next)
				{
					_rightLaneChange = laneChange.gameObject;
					_rightLaneChange.SetActive (false);
				}
			}
		}

		if(_leftLaneChange == null)
			Debug.LogWarning ("No Left Lane Change!: " + name);

		if(_rightLaneChange == null)
			Debug.LogWarning ("No Right Lane Change!: " + name);

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
