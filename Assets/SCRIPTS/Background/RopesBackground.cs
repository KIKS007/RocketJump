﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopesBackground : MonoBehaviour 
{
	[Header ("Infos")]
	public float FirstY;
	public float BottomLimit;

	[Header ("Settings")]
	public float Height;
	public float Speed;

	private ParallaxCamera _parallaxCamera;

	// Use this for initialization
	void Start () 
	{
		_parallaxCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ParallaxCamera> ();

		int[] randomPos = new int[transform.childCount];

		for (int i = 0; i < randomPos.Length; i++)
			randomPos [i] = Random.Range (0, transform.childCount);

		for (int i = 0; i < transform.childCount; i++)
			transform.GetChild (i).SetSiblingIndex (randomPos [i]);

		for(int i = 0; i < transform.childCount; i++)
		{
			Vector3 position = transform.GetChild (transform.childCount - 1 - i).position;
			position.y = FirstY + Height * i;
			transform.GetChild (transform.childCount - i - 1).position = position;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform highestRope = transform.GetChild (0);

		if(GameManager.Instance.GameState == GameState.Playing)
		{
			foreach (Transform child in transform)
				if (child.position.y > highestRope.position.y)
					highestRope = child;

			foreach(Transform child in transform)
			{
				//child.Translate (Vector3.down * Speed * Time.deltaTime);

				child.Translate (Vector3.down * Speed * _parallaxCamera.delta * Time.deltaTime);
				
				if(child.position.y < BottomLimit)
				{
					Vector3 position = child.position;
					position.y = highestRope.position.y + Height;
					child.position = position;
				}
			}
		}
	}
}
