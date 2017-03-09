using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour 
{
	private Player _playerScript;

	// Use this for initialization
	void Start () 
	{
		_playerScript = transform.parent.GetComponent<Player> ();
	}
	
	void OnTriggerEnter (Collider collider)
	{
		if ((_playerScript.GroundLayer.value & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
		{
			if (collider.transform.GetComponentInChildren<PassThroughPlatform> () != null && _playerScript._rigidbody.velocity.y > 0)
				return;
			
			_playerScript.Grounded ();
		}
	}

	void OnTriggerExit (Collider collider)
	{
		if ((_playerScript.GroundLayer.value & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
			_playerScript.InAir ();
	}
}
