using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour 
{
	private int Value = 2;

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Player" && collider.GetComponent<Rigidbody> () != null)
		{
			CollectiblesManager.Instance.CollectiblePickedUp (Value);
			ScoreManager.Instance.PickupCollected (Value);
			Destroy (gameObject);
		}
	}
}
