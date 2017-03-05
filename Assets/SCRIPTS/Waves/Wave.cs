using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class Wave : MonoBehaviour 
{
	[Header ("Wave Force")]
	public Vector2 WaveForceLimits;
	public float MaxForceDuration;

	[Header ("Gravity")]
	public float GravityForce;

	[Header ("Mixtape")]
	public float MixtapeDuration = 10f;

	[Header ("Rocket")]
	public GameObject Rocket;

	[Header ("Music")]
	[SoundGroup]
	public string Music;

	[Header ("Sounds")]
	[SoundGroup]
	public string WaveSound;
}
