using System.Collections;
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

	public Transform rocketWave;
	public GameObject explosion;
	public GameObject ball;

	// Use this for initialization
	protected virtual void Awake () 
	{
		SetupRigidbody ();

		if(rocketWave != null)
			rocketWave.SetParent (null);
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
		Destroy (ball);
		Instantiate (explosion, transform.position, Quaternion.identity);

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

	protected virtual void ExplosionDebug ()
	{
		GetComponent<Renderer> ().enabled = false;

		GetComponent<Collider> ().isTrigger = true;
		_rigidbody.velocity = Vector3.zero;
		transform.DOScale (ExplosionRadius, 0.4f).OnComplete (End);
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

