using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public float CurrentHeight = 0;

	[Header ("Follow")]
	public float SmoothTime;
	public Vector3 Offset;

	private Transform _player;
	private Vector3 _velocity = Vector3.zero;
	private float _bottomMarging = 10;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		CurrentHeight = _player.position.y;

		transform.position = new Vector3 (transform.position.x, _player.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Follow ();	
	}

	void Follow ()
	{
		Vector3 target = transform.position;
		float heightTemp = CurrentHeight;

		//if (_player.position.y > CurrentHeight)
			CurrentHeight = _player.position.y;
		
		/*else if (_player.position.y > CurrentHeight - _bottomMarging)
			heightTemp = _player.position.y;*/
			
		target.y = heightTemp;
		target += Offset;

		transform.position = Vector3.SmoothDamp (transform.position, target, ref _velocity, SmoothTime);
	}
}
