using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour 
{
	void OnTriggerEnter (Collider collider)
	{
		if(collider.tag == "Player" && GameManager.Instance.GameState == GameState.Playing)
		{
			GameManager.Instance.GameOver ();
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "Player" && GameManager.Instance.GameState == GameState.Playing)
		{
			GameManager.Instance.GameOver ();
		}
	}
}
