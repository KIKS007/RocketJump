using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public delegate void EventHandler();

public enum WaveState {CanWave, IsWaving, Cooldown};
public enum JumpState {InAir, Grounded};

public class Player : MonoBehaviour 
{
	[Header ("Player States")]
	public WaveState WaveState = WaveState.CanWave;
	public JumpState JumpState = JumpState.Grounded;

	[Header ("Wave")]
	public Wave CurrentWave;
	[Range (0, 1)]
	public float WaveForceDebug;

	[Header ("Rocket Launch")]
	public Transform LaunchPoint;
	public GameObject CurrentRocket;

	[Header ("Crosshairs")]
	public Transform Crosshairs;

	[Header ("Grounded")]
	public LayerMask GroundLayer;

	private Camera _mainCamera;
	private Vector3 _launchPosition;
	private Rigidbody _rigidbody;
	private float _waveForce;
	private SlowMotion _slowMotion;

	public event EventHandler OnWaveChange;
	public event EventHandler OnRocketChange;

	// Use this for initialization
	void Start () 
	{
		_mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		_slowMotion = _mainCamera.GetComponent<SlowMotion> (); 
		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetInput ();

		SetCrossHair ();

		Grounded ();
	}

	void FixedUpdate ()
	{
		Gravity ();
	}

	void Gravity ()
	{
		_rigidbody.AddForce (Vector3.down * CurrentWave.GravityForce, ForceMode.Force);
	}

	void GetInput ()
	{
		if (Input.GetMouseButtonDown (0) && WaveState == WaveState.CanWave)
			WaveForce ();

		if(Input.GetMouseButtonUp (0) && WaveState == WaveState.IsWaving)
			Wave ();

		if(Input.GetMouseButton (0))
			Debug.DrawRay (transform.position, _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z)), Color.red);
	}

	void WaveForce ()
	{
		_slowMotion.StartSlowMotion ();

		WaveState = WaveState.IsWaving;

		_rigidbody.velocity = Vector3.zero;
		_waveForce = CurrentWave.WaveForceLimits.x;

		DOTween.To (()=> _waveForce, x => _waveForce = x, CurrentWave.WaveForceLimits.y, CurrentWave.MaxForceDuration)
			.SetEase (Ease.OutQuad)
			.OnUpdate (()=> WaveForceDebug = _waveForce / CurrentWave.WaveForceLimits.y).SetId ("Wave")
			.OnComplete (()=> { if(WaveState == WaveState.IsWaving) Wave(); } );
	}

	void Wave ()
	{
		DOTween.Kill ("Wave");

		_launchPosition = _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
		_rigidbody.velocity = Vector3.zero;

		LaunchRocket ();

		Vector3 recoilDirection = transform.position - _launchPosition;
		recoilDirection.z = 0;
		recoilDirection.Normalize ();

		_rigidbody.AddForce (recoilDirection * _waveForce, ForceMode.Impulse);
		_waveForce = 0;
		WaveForceDebug = 0;

		JumpState = JumpState.InAir;

		_slowMotion.StopSlowMotion ();
		StartCoroutine (WaveCooldown ());
	}

	IEnumerator WaveCooldown ()
	{
		WaveState = WaveState.Cooldown;

		yield return new WaitForSecondsRealtime (CurrentWave.WaveCooldown);

		WaveState = WaveState.CanWave;
	}

	void SetCrossHair ()
	{
		if(WaveState == WaveState.IsWaving)
		{
			if(!Crosshairs.gameObject.activeSelf)
				Crosshairs.gameObject.SetActive (true);

			Vector3 direction = transform.position - _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));

			Crosshairs.position = transform.position + direction.normalized * 3;
		}
		else if(Crosshairs.gameObject.activeSelf)
			Crosshairs.gameObject.SetActive (false);
	}

	void LaunchRocket ()
	{
		if (CurrentRocket == null)
			return;

		GameObject rocket = Instantiate (CurrentRocket, LaunchPoint.position, Quaternion.identity) as GameObject;

		rocket.transform.LookAt (new Vector3 (_launchPosition.x, _launchPosition.y, 0));

		float launchForce = rocket.GetComponent<Rocket> ().LaunchForce;
		Rigidbody bodyRigidbody = rocket.GetComponent<Rocket> ()._rigidbody;

		bodyRigidbody.AddForce (rocket.transform.forward * launchForce, ForceMode.Impulse);
	}

	void Grounded ()
	{
		Vector3 position = transform.position;
		position.y -= 0.8f;

		if (Physics.CheckSphere (position, 0.4f, GroundLayer, QueryTriggerInteraction.Ignore))
			JumpState = JumpState.Grounded;
		else
			JumpState = JumpState.InAir;
	}

	public void SetWave (Wave wave)
	{
		CurrentWave = wave;

		if (CurrentWave.Rocket != null)
			SetRocket (CurrentWave.Rocket);
		else
			Debug.LogWarning ("No Rocket!");

		if (OnWaveChange != null)
			OnWaveChange ();
	}

	public void SetRocket (GameObject rocket)
	{
		CurrentRocket = rocket;

		if (OnRocketChange != null)
			OnRocketChange ();
	}
}
