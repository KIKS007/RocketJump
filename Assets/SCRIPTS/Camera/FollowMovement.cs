
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowMovement : MonoBehaviour 
{
	public float CurrentHeight = 0;

	[Header ("Rise")]
	public float Speed;

	[Header ("Follow")]
	public float SmoothTime;
	public Vector3 Offset;

	private Transform _player;
	private Rigidbody _playerRigidbody;

	private Vector3 _velocity = Vector3.zero;

	private float _velocityThreshold = 0.5f;

	// Use this for initialization
	void Awake () 
	{
		GameManager.Instance.OnPlaying += Setup;
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

		CurrentHeight = _player.position.y;

		transform.position = new Vector3 (0, _player.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(GameManager.Instance.GameState == GameState.Playing)
		{
			if (_player == null)
				return;

			if(_playerRigidbody.velocity.magnitude < _velocityThreshold)
				Rise ();
			
			else
				Follow ();
		}
	}

	void Follow ()
	{
		Vector3 target = transform.position;

		if (_player.position.y > CurrentHeight)
			CurrentHeight = _player.position.y;
			
		target.y = CurrentHeight;
		target += Offset;

		transform.position = Vector3.SmoothDamp (transform.position, target, ref _velocity, SmoothTime);
	}

	void Rise ()
	{
		transform.Translate (Vector3.up * Speed * Time.deltaTime);
	}
}
