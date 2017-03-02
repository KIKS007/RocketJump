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

	public GameObject _rightLaneChange;
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
}
