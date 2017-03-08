using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeadZone : MonoBehaviour 
{
	void OnTriggerEnter (Collider collider)
	{
		if(collider.tag == "Player" && GameManager.Instance.GameState == GameState.Playing)
		{
			if(transform.childCount > 0)
				transform.GetChild (0).DOShakeScale (0.8f, new Vector3 (1.5f, 1.5f, 0));
			GameManager.Instance.GameOver ();
		}
	}

	void OnCollisionEnter (Collision collision)
	{
		if(collision.gameObject.tag == "Player" && GameManager.Instance.GameState == GameState.Playing)
		{
			if(transform.childCount > 0)
				transform.GetChild (0).DOShakeScale (0.8f, new Vector3 (1.5f, 1.5f, 0));
			GameManager.Instance.GameOver ();
		}
	}
}
