﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rocket : MonoBehaviour 
{
	[Header ("Launch")]
	public float LaunchForce;

	[Header ("Collision")]
	public LayerMask CollisionLayer = (1 << 9) | (1 << 10) | (1 << 11) | (1 << 12);

	[Header ("Explosion")]
	public LayerMask ExplosionLayer = (1 << 11);
	public float ExplosionForce;
	public float ExplosionRadius;

	[HideInInspector]
	public Rigidbody _rigidbody;

	// Use this for initialization
	protected virtual void Awake () 
	{
		SetupRigidbody ();
	}

	protected virtual void SetupRigidbody ()
	{
		_rigidbody = GetComponent<Rigidbody> ();	
	}

	protected virtual void OnCollisionEnter (Collision collision)
	{
		if ((CollisionLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
			Explode ();

		if ((ExplosionLayer.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
			KillEnemy (collision.gameObject);
	}

	protected virtual void KillEnemy (GameObject enemy)
	{
		//Enemy Killed
	}

	public virtual void Explode ()
	{
		foreach(Collider other in Physics.OverlapSphere(transform.position, ExplosionRadius, ExplosionLayer))
		{
			Vector3 repulseDirection = other.transform.position - transform.position;
			repulseDirection.Normalize ();

			float explosionImpactZone = 1 - (Vector3.Distance (transform.position, other.transform.position) / ExplosionRadius);

			if(explosionImpactZone > 0)
			{
				Vector3 appliedForce = repulseDirection * explosionImpactZone * ExplosionForce;
				appliedForce.z = 0;

				if(other.GetComponent<Rigidbody>() != null)
					other.GetComponent<Rigidbody> ().AddForce (appliedForce, ForceMode.Impulse);
			}
		}

		ExplosionDebug ();
		//Die ();
	}

	void ExplosionDebug ()
	{
		GetComponent<Collider> ().enabled = false;
		_rigidbody.velocity = Vector3.zero;
		transform.DOScale (ExplosionRadius, 0.5f).OnComplete (End);
	}

	public virtual void End ()
	{
		Destroy (gameObject);
	}

	void OnBecameInvisible () 
	{
		if (gameObject)
			End ();
	}
}

