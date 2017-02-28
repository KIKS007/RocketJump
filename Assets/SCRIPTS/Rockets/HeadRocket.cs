using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRocket : Rocket 
{
	[Header ("Head Gravity")]
	public float HeadGravityForce;
	protected Rigidbody _headRigidbody;

	protected override void SetupRigidbody ()
	{
		_rigidbody = transform.GetChild (0).GetComponent<Rigidbody> ();	
		_headRigidbody = transform.GetChild (1).GetComponent<Rigidbody> ();	
		//_rigidbody.centerOfMass = new Vector3 (0, 0, 1);
	}

	void FixedUpdate ()
	{
		Gravity ();
	}

	void Gravity ()
	{
		//_bodyRigidbody.AddForceAtPosition (Vector3.down * BodyGravityForce, new Vector3 (0, 0, transform.lossyScale.z * 0.8f), ForceMode.Force);
		_headRigidbody.AddForce (Vector3.down * HeadGravityForce, ForceMode.Force);
	}
}
