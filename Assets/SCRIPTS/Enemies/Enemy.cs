using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	private static float _jumpForce = 10;
	private Rigidbody _playerRigidbody;

	// Use this for initialization
	void Start () 
	{
		_playerRigidbody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	protected virtual void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "Player")
			HitByPlayer ();

		if (LayerMask.NameToLayer ("Rocket") == collision.gameObject.layer)
			Death ();
	}

	protected virtual void HitByPlayer ()
	{
		_playerRigidbody.AddForce (Vector3.up * _jumpForce, ForceMode.Impulse);

		Death ();
	}

	protected virtual void Death ()
	{
		Destroy (gameObject);
	}
}
