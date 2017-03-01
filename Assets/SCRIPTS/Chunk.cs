using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum WallType { Solid, Breakable, Opened };
public enum LaneType { First, Second, Third };

public class Chunk : MonoBehaviour 
{
	[Header ("Lane Type")]
	public LaneType Lane;

	[Header ("Wall Type")]
	public WallType RightWall;
	public WallType LeftWall;

	public List<GameObject> RightBreakableBloc = new List<GameObject>();
	public List<GameObject> LeftBreakableBloc = new List<GameObject>();

	//[HideInInspector]

	void Awake ()
	{
		if(RightWall != WallType.Solid)
		{
			if(Lane == LaneType.Second)
				FindObjectwithTag ("BreakableBloc", RightBreakableBloc, x => x.transform.position.x > transform.position.x);
			
			if(Lane == LaneType.First)
				FindObjectwithTag ("BreakableBloc", RightBreakableBloc);
		}

		if(LeftWall != WallType.Solid)
		{
			if(Lane == LaneType.Second)
				FindObjectwithTag ("BreakableBloc", LeftBreakableBloc, x => x.transform.position.x < transform.position.x);
			
			if(Lane == LaneType.Third)
				FindObjectwithTag ("BreakableBloc", LeftBreakableBloc);
			
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
