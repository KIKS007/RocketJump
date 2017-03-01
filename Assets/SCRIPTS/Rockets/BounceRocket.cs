using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BounceRocket : Rocket 
{
	[Header ("Bounce")]
	public float BounceScale = 1.2f;
	public float BounceDuration = 0.2f;
	public int BounceVibrato = 10;
	[Range (0, 1)]
	public float BounceElasticity = 1;

	private Vector3 _initialScale;

	protected override void Awake ()
	{
		base.Awake ();

		_initialScale = transform.localScale;
	}

	protected override void OnCollisionEnter (Collision collision)
	{
		base.OnCollisionEnter (collision);

		if(!DOTween.IsTweening ("Bounce" + GetInstanceID ()))
		{
			transform.localScale = _initialScale;
			
			transform.DOPunchScale (new Vector3 (BounceScale, BounceScale, BounceScale), BounceDuration, BounceVibrato, BounceElasticity).SetId ("Bounce" + GetInstanceID ());
		}
	}

	protected override void ExplosionDebug ()
	{
		//Nothing
	}
}
