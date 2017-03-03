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
		
		if (LayerMask.NameToLayer ("Rocket") == collision.gameObject.layer)
			Death ();

        if (collision.gameObject.tag == "Player")
            HitByPlayer();

    }

	protected virtual void HitByPlayer ()
	{
        GameManager.Instance.GameOver();
	}

	protected virtual void Death ()
	{
		Destroy (gameObject);
	}
}
