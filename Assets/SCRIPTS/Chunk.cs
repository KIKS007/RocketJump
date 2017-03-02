using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WallType { Solid, Breakable };
public enum LaneType { First, Second, Third };

public class Chunk : MonoBehaviour 
{
	[HideInInspector]
	public LaneType Lane;

	[Header ("Wall Type")]
	public WallType RightWall;
	public WallType LeftWall;

	[Header ("Breakable Blocs")]
	public List<GameObject> RightBreakableBlocs = new List<GameObject>();
	public List<GameObject> LeftBreakableBlocs = new List<GameObject>();

	private GameObject _rightLaneChange;
	private GameObject _leftLaneChange;

	void Awake ()
	{
		if(RightWall != WallType.Solid)
		{
			if(Lane == LaneType.Second)
				FindObjectwithTag ("BreakableBloc", RightBreakableBlocs, x => x.transform.position.x > transform.position.x);
			
			if(Lane == LaneType.First)
				FindObjectwithTag ("BreakableBloc", RightBreakableBlocs);
		}

		if(LeftWall != WallType.Solid)
		{
			if(Lane == LaneType.Second)
				FindObjectwithTag ("BreakableBloc", LeftBreakableBlocs, x => x.transform.position.x < transform.position.x);
			
			if(Lane == LaneType.Third)
				FindObjectwithTag ("BreakableBloc", LeftBreakableBlocs);
		}

		switch (Lane)
		{
		case LaneType.First:
			if (RightWall != WallType.Solid)
			{
				if (transform.GetComponentInChildren<LaneChange> () != null)
					_rightLaneChange = transform.GetComponentInChildren<LaneChange> ().gameObject;
				else
					Debug.LogWarning ("No Right Lane Change!");
			}
			break;

		case LaneType.Second:
			if (RightWall != WallType.Solid || LeftWall != WallType.Solid)
			{
				LaneChange[] lanesChanges = transform.GetComponentsInChildren<LaneChange> ();
				
				if(lanesChanges.Length > 0)
				{
					foreach(LaneChange laneChange in lanesChanges)
					{
						if(laneChange.Change == LaneChange.ChangeType.SecondToFirst)
							_leftLaneChange = laneChange.gameObject;
						
						if(laneChange.Change == LaneChange.ChangeType.SecondToThird)
							_rightLaneChange = laneChange.gameObject;
					}
				}
				else
					Debug.LogWarning ("No Right Lane Change!");
			}
			break;

		case LaneType.Third:
			if (LeftWall != WallType.Solid)
			{
				if (transform.GetComponentInChildren<LaneChange> () != null)
					_leftLaneChange = transform.GetComponentInChildren<LaneChange> ().gameObject;
				else
					Debug.LogWarning ("No Left Lane Change!");
			}
			break;
		}
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
