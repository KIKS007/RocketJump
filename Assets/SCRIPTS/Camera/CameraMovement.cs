using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	[Header ("Follow")]
	public float SmoothTime;
	public Vector3 Offset;

	private Transform _player;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Follow ();	
	}

	void Follow ()
	{
		Vector3 target = transform.position;
		target.y = _player.position.y;
		target += Offset;

		transform.position = Vector3.SmoothDamp (transform.position, target, ref velocity, SmoothTime);
	}
}
