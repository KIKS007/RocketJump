using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour 
{
	[Header ("Wave Force")]
	public Vector2 WaveForceLimits;
	public float MaxForceDuration;

	[Header ("Cooldown")]
	public float WaveCooldown;

	[Header ("Gravity")]
	public float GravityForce;
}
