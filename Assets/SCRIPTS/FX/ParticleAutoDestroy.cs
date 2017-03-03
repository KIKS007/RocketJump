using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour 
{
	private ParticleSystem particle;

	/*// Use this for initialization
	void Start () 
	{
		particle = GetComponent<ParticleSystem> ();

		StartCoroutine (Wait ());
	}

	IEnumerator Wait ()
	{
		yield return new WaitWhile (() => particle.IsAlive ());

		Destroy (gameObject);
	}*/
}
