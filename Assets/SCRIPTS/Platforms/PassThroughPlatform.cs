using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour 
{
	//public bool PassThroughOn;

	private Collider _parentCollider;
	private Collider _playerCollider;

	void Awake ()
	{
		gameObject.layer = LayerMask.NameToLayer ("Default");

		_parentCollider = transform.parent.GetComponent<Collider> ();
		_playerCollider = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider> ();
	}

	void OnTriggerEnter (Collider collider)
	{
		if(collider.tag == "Player")
			Physics.IgnoreCollision (_playerCollider, _parentCollider, true);
	}

	void OnTriggerExit (Collider collider)
	{
		if(collider.tag == "Player")
			Physics.IgnoreCollision (_playerCollider, _parentCollider, false);
	}
}
