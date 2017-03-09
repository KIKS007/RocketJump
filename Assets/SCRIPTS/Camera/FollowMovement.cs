
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FollowMovement : MonoBehaviour 
{
	private float InitialYPos = -7;

	[Header ("Follow")]
	public float SmoothTime;
	public float DownSmoothTime;
	public Vector3 Offset;

	private Transform _player;

	private Vector3 _velocity = Vector3.zero;

	// Use this for initialization
	void Awake () 
	{
		GameManager.Instance.OnPlaying += Setup;
		GameManager.Instance.OnMenu += ()=> transform.position = new Vector3 (0, InitialYPos, transform.position.z);
		ScoreManager.Instance.InitialPosition = InitialYPos + 1;
	}

	void Setup ()
	{
		if(GameObject.FindGameObjectWithTag ("Player") == null)
		{
			Debug.LogWarning ("No Player Found!");
			return;
		}

		_player = GameObject.FindGameObjectWithTag ("Player").transform;

		transform.position = new Vector3 (0, InitialYPos, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(GameManager.Instance.GameState == GameState.Playing)
		{
			if (_player == null)
				return;

			Follow ();
		}
	}

	void Follow ()
	{
		Vector3 target = transform.position;
	
		target.y = _player.position.y;
		target += Offset;

		float smoothTime = target.y > transform.position.y ? SmoothTime : DownSmoothTime;

		transform.position = Vector3.SmoothDamp (transform.position, target, ref _velocity, smoothTime);
	}

}
