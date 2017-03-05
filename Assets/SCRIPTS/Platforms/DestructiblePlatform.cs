using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class DestructiblePlatform : MonoBehaviour 
{
	private static float DestroyDuration = 0.2f;

	private string DestroySound = "SFX_BreakPlatform";

	void OnTriggerEnter (Collider collider)
	{
		if (collider.gameObject.tag == "Player")
			Remove ();

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

		if (GetComponent<Collider> () != null) 
		{
			GetComponent<Collider> ().enabled = false;
			transform.DOScale (0, DestroyDuration).SetEase (Ease.OutQuad).OnComplete (()=> Destroy (gameObject));
		} 
		else
			Debug.LogWarning ("No Destructible Collider!");
	}

}
