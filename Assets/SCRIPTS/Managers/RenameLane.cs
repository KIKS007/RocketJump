using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenameLane : MonoBehaviour 
{
	void OnEnable ()
	{
		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild (i).name = "Chuck : " + i;
	}
}
