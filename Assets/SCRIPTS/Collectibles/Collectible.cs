using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour 
{
	public int Value = 1;

	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.tag == "Player")
			CollectiblesManager.Instance.CollectiblePickedUp (Value);

		Destroy (gameObject);
	}
}
