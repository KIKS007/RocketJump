using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Background : MonoBehaviour 
{
	public float duration;
	public float finaleYPos;

	void Start ()
	{
		transform.DOLocalMoveY (finaleYPos, duration);
	}
}
