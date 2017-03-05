using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserRocket : Rocket 
{
	[Header ("Laser")]
	public float LaserScale = 15;
	public float LaserDuration = 0.5f;
	public Ease LaserEase = Ease.OutQuad;

	public ParticleSystem laser;

	protected override void Start ()
	{
		transform.GetChild (0).DOScaleZ (LaserScale, LaserDuration).SetEase (LaserEase)
			.OnComplete (()=> transform.DOScaleY (0f, 0.2f).OnComplete (End));

		laser.Play ();
	}

 	protected override void OnCollisionEnter (Collision collision)
	{
		
	}

	protected void OnTriggerEnter (Collider collider)
	{
		if ((ExplosionLayer.value & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
			KillEnemy (collider.gameObject);
	}
}
