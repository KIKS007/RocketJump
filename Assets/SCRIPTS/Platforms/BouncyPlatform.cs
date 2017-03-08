using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BouncyPlatform : MonoBehaviour 
{
	void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == ("Player"))
			transform.GetChild (0).DOShakeScale (0.8f, new Vector3(0, 1, 0));
	}
}
