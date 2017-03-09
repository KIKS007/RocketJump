using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class Collectible : MonoBehaviour 
{
	public GameObject FX;

	private int Value = 2;

	private bool pickedUp = false;

	private string SFX = "SFX_PickUpBonus";

	void OnTriggerEnter (Collider collider)
	{
		if (collider.tag == "Player" && collider.GetComponent<Rigidbody> () != null || collider.gameObject.tag == "Rocket")
		{
			if (pickedUp)
				return;

			pickedUp = true;

			Instantiate (FX, transform.position, Quaternion.identity);

			MasterAudio.PlaySoundAndForget (SFX);

			VibrationManager.Instance.Vibrate (FeedbackType.Jump);

			CollectiblesManager.Instance.CollectiblePickedUp (Value);
			ScoreManager.Instance.PickupCollected (Value);
			ScoreManager.Instance.PopupScore (transform, Value, 0.8f);

			transform.DOScale (0, 0.25f).OnComplete (()=> Destroy (gameObject));
		}
	}
}
