using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketCollectible : MonoBehaviour {

	public GameObject newRocket;
	public float duration = 15f;

	private GameObject oldRocket;

	void OnTriggerEnter (Collider collider)
	{
		if(collider.tag == "Player")
		{
			oldRocket = collider.GetComponent<Player> ().CurrentRocket;
			collider.GetComponent<Player> ().CurrentRocket = newRocket;

			GetComponent<Collider> ().enabled = false;
			Destroy (transform.GetChild (0).gameObject);

			StartCoroutine (Wait ());
		}
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSecondsRealtime (duration);

		if(GameObject.FindGameObjectWithTag ("Player") != null)
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().CurrentRocket = oldRocket;
	}
}
