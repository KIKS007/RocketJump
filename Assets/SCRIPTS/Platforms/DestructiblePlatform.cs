using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class DestructiblePlatform : MonoBehaviour 
{
	public GameObject Fx;

	private static float DestroyDuration = 0.2f;

	private string DestroySound = "SFX_BreakPlatform";

	void OnTriggerEnter (Collider collider)
	{
		if (LayerMask.NameToLayer ("Rocket") == collider.gameObject.layer)
			Remove ();
	}

	void OnCollisionEnter (Collision collision)
	{
		if (LayerMask.NameToLayer ("Rocket") == collision.gameObject.layer)
			Remove ();
	}

	void Remove ()
	{
		MasterAudio.PlaySoundAndForget (DestroySound);

		Instantiate (Fx, transform.position, Quaternion.identity);

		ScoreManager.Instance.PopupScore (transform, 10, 1);
		ScoreManager.Instance.PickupCollected (10);

		if (GetComponent<Collider> () != null) 
		{
			GetComponent<Collider> ().enabled = false;
			transform.DOScale (0, DestroyDuration).SetEase (Ease.OutQuad).OnComplete (()=> Destroy (gameObject));
		} 
		else
			Debug.LogWarning ("No Destructible Collider!");
	}

}
