
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowMovement : MonoBehaviour 
{
	public float CurrentHeight = 0;

	[Header ("Rise")]
	public float RiseLerp = 0.1f;
	public float RiseValue = 3;

	[Header ("Follow")]
	public float SmoothTime;
	public float DownSmoothTime;
	public Vector3 Offset;

	private Transform _player;
	private Rigidbody _playerRigidbody;

	private Vector3 _velocity = Vector3.zero;

	private float _velocityThreshold = 0.5f;
	private float _initialYOffset;

	// Use this for initialization
	void Awake () 
	{
		GameManager.Instance.OnPlaying += Setup;
		GameManager.Instance.OnMenu += ()=> transform.position = new Vector3 (0, 0, transform.position.z);

		_initialYOffset = Offset.y;
	}

	void Setup ()
	{
		if(GameObject.FindGameObjectWithTag ("Player") == null)
		{
			Debug.LogWarning ("No Player Found!");
			return;
		}

		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_playerRigidbody = _player.GetComponent<Rigidbody> ();

		transform.position = new Vector3 (0, _player.position.y + Offset.y, transform.position.z);
		ScoreManager.Instance.InitialPosition = transform.position.y;

		CurrentHeight = _player.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(GameManager.Instance.GameState == GameState.Playing)
		{
			if (_player == null)
				return;

			Follow ();

			if(_playerRigidbody.velocity.y > 0)
				Offset.y = Mathf.Lerp (Offset.y, RiseValue, RiseLerp);
			else
				Offset.y = Mathf.Lerp (Offset.y, _initialYOffset, RiseLerp);
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
