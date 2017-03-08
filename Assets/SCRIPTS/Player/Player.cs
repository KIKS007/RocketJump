using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public delegate void EventHandler();

public enum WaveState {CanWave, IsWaving, HasWaved};
public enum JumpState {InAir, Grounded};

public class Player : MonoBehaviour 
{
	[Header ("Player States")]
	public WaveState WaveState = WaveState.CanWave;
	public JumpState JumpState = JumpState.Grounded;

	[Header ("Inputs")]
	public float MouseHeldWindow = 0.1f;

	[Header ("Wave")]
	public Wave CurrentWave;
	[Range (0, 1)]
	public float WaveForceDebug;

	[Header ("Rocket Launch")]
	public Transform RocketsParent;
	public Transform LaunchPoint;
	public GameObject CurrentRocket;

	[Header ("Crosshairs")]
	public Transform Crosshairs;
	public LineRenderer CrossHairRenderer;

	[Header ("Grounded")]
	public LayerMask GroundLayer;

	[Header ("Death")]
	public GameObject deathParticle;

	private Camera _mainCamera;
    [HideInInspector]
    public Vector3 _launchPosition;
	private Rigidbody _rigidbody;
	private float _waveForce;
	private SlowMotion _slowMotion;
    [HideInInspector]
    public float _launchDelay = 0.005f;

	public event EventHandler OnWaveChange;
	public event EventHandler OnRocketChange;
	public event EventHandler OnGrounded;
    public event EventHandler OnLaunch;

	public event EventHandler OnJump;
    public event EventHandler OnHold;

	private Coroutine _waitInput;

    public bool cantInput = false;
    public bool cantRocket = false;

    // Use this for initialization
    void Start () 
	{
		_mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		_slowMotion = _mainCamera.GetComponent<SlowMotion> (); 
		_rigidbody = GetComponent<Rigidbody> ();

		CrossHairRenderer.gameObject.SetActive (true);
		CrossHairRenderer.startWidth = 0;

		OnHold += EnableArrow;
		OnJump += DisableArrow;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (GameManager.Instance.GameState == GameState.Playing) 
		{
			SetCrossHair ();

            if (!cantInput)
            {
                GetInput();
            }
            Grounded ();
		}
	}

	void FixedUpdate ()
	{
		if(GameManager.Instance.GameState == GameState.Playing)
			Gravity ();
	}

	void Gravity ()
	{
		if (CurrentWave == null)
			return;

		_rigidbody.AddForce (Vector3.down * CurrentWave.GravityForce, ForceMode.Force);
	}

	void GetInput ()
	{
		if (CurrentWave == null)
			return;

		if (Input.GetMouseButtonDown (0) && WaveState == WaveState.CanWave)
			_waitInput = StartCoroutine (WaitInput ());

		if (Input.GetMouseButtonUp (0) && WaveState != WaveState.IsWaving)
		{
			if(_waitInput != null)
				StopCoroutine (_waitInput);

			if(WaveState != WaveState.HasWaved)
				LaunchRocket ();
			
			WaveState = WaveState.CanWave;
		}

		else if (Input.GetMouseButtonUp (0) && WaveState == WaveState.IsWaving)
		{
			if(_waitInput != null)
				StopCoroutine (_waitInput);
			
			Wave ();
			WaveState = WaveState.CanWave;
		}
	}

	IEnumerator WaitInput ()
	{
		_launchPosition = _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));

		yield return new WaitForSecondsRealtime (MouseHeldWindow);

		if (Input.GetMouseButton (0) && WaveState == WaveState.CanWave)
			WaveForce ();
		else
			LaunchRocket ();
	}

	void WaveForce ()
	{
        if (OnHold != null)
			OnHold();

        _slowMotion.StartSlowMotion ();

		WaveState = WaveState.IsWaving;

		_rigidbody.velocity = Vector3.zero;
		_waveForce = CurrentWave.WaveForceLimits.x;

		DOTween.To (()=> _waveForce, x => _waveForce = x, CurrentWave.WaveForceLimits.y, CurrentWave.MaxForceDuration)
			.SetEase (Ease.OutQuad)
			.OnUpdate (()=> WaveForceDebug = _waveForce / CurrentWave.WaveForceLimits.y).SetId ("Wave")
			.SetUpdate (true)
			.OnComplete (()=> { 
				if(WaveState == WaveState.IsWaving && GameManager.Instance.GameState != GameState.GameOver) 
					Wave();
			});
	}

	void Wave ()
	{
		DOTween.Kill ("Wave");

		_launchPosition = _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
		_rigidbody.velocity = Vector3.zero;

       //LaunchRocket ();

		Vector3 recoilDirection = transform.position - _launchPosition;
		recoilDirection.z = 0;
		recoilDirection.Normalize ();

		_mainCamera.GetComponent<ScreenShakeCamera> ().CameraShaking (FeedbackType.Jump);
		VibrationManager.Instance.Vibrate (FeedbackType.Jump);

		_rigidbody.AddForce (recoilDirection * _waveForce, ForceMode.Impulse);
		_waveForce = 0;
		WaveForceDebug = 0;

		JumpState = JumpState.InAir;

		if (OnJump != null)
			OnJump ();

		_slowMotion.StopSlowMotion ();

		WaveState = WaveState.HasWaved;

		MasterAudio.PlaySoundAndForget (CurrentWave.WaveSound);
	}

	void SetCrossHair ()
	{
		Vector3 direction = transform.position - _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
		Crosshairs.position = transform.position + direction.normalized * 3;
		Crosshairs.LookAt (transform.position);

		CrossHairRenderer.SetPosition (0, transform.position + direction.normalized * 0.2f);
		CrossHairRenderer.SetPosition (1, transform.position + direction.normalized * 5);

		if(WaveState == WaveState.IsWaving)
		{
			if(!Crosshairs.gameObject.activeSelf)
				Crosshairs.gameObject.SetActive (true);

		}
		else if(Crosshairs.gameObject.activeSelf)
		{
			Crosshairs.gameObject.SetActive (false);
		}
	}

	void LaunchRocket ()
	{
		if (CurrentRocket == null)
			return;

        if (OnLaunch != null)
            OnLaunch();

        if (cantRocket)
            return;
       
            GameObject rocket = Instantiate(CurrentRocket, LaunchPoint.position, Quaternion.identity, RocketsParent) as GameObject;

            rocket.transform.LookAt(_launchPosition);
            //rocket.transform.LookAt (Crosshairs.position);

            float launchForce = rocket.GetComponent<Rocket>().LaunchForce;
            Rigidbody bodyRigidbody = rocket.GetComponent<Rocket>()._rigidbody;

            bodyRigidbody.AddForce(rocket.transform.forward * launchForce, ForceMode.Impulse);  
	}

	void Grounded ()
	{
		Vector3 position = transform.position;
		position.y -= 0.8f;

		if (Physics.CheckSphere (position, 0.2f, GroundLayer, QueryTriggerInteraction.Ignore))
		{
			if(JumpState == JumpState.InAir && OnGrounded != null)
				OnGrounded ();
			
			JumpState = JumpState.Grounded;
		}
		else
			JumpState = JumpState.InAir;
	}

	void EnableArrow ()
	{
		CrossHairRenderer.gameObject.SetActive (true);
		DOTween.To (() => CrossHairRenderer.startWidth, x => CrossHairRenderer.startWidth = x, 1, 0.5f).SetUpdate (true);
	}

	void DisableArrow ()
	{
		CrossHairRenderer.startWidth = 0;
		CrossHairRenderer.gameObject.SetActive (false);
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
