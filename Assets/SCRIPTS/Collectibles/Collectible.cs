using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectible : MonoBehaviour 
{
	public GameObject FX;

	private int Value = 2;

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Player" && collider.GetComponent<Rigidbody> () != null || collider.gameObject.tag == "Rocket")
		{
			Instantiate (FX, transform.position, Quaternion.identity);

			VibrationManager.Instance.Vibrate (FeedbackType.Jump);

			CollectiblesManager.Instance.CollectiblePickedUp (Value);
			ScoreManager.Instance.PickupCollected (Value);

			transform.DOScale (0, 0.25f).OnComplete (()=> Destroy (gameObject));
		}
	}
}
