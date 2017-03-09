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

	[Header ("Wave Force")]
	public Vector2 WaveForceLimits = new Vector2 (15, 20);
	public float MaxForceDuration = 1.2f;
	[SoundGroup]
	public string WaveSound;
	public GameObject WaveFX;

	[Header ("Gravity")]
	public float GravityForce = 15;

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
	[HideInInspector]
	public Rigidbody _rigidbody;
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

	private float _crossHairMin = 2;
	private float _crossHairMax = 4;
	private float _crossHairDistance;

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

		#if UNITY_ANDROID && !UNITY_EDITOR
		Debug.Log ("ANDROID");
		#else
		Debug.Log ("PC");
		#endif
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (GameManager.Instance.GameState == GameState.Playing) 
		{

            if (!cantInput)
            {
				SetCrossHair ();
                GetInput();
            }
		}
	}

	void FixedUpdate ()
	{
		if(GameManager.Instance.GameState == GameState.Playing)
			Gravity ();
	}

	void Gravity ()
	{
		_rigidbody.AddForce (Vector3.down * GravityForce, ForceMode.Force);
	}

	void GetInput ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began && WaveState == WaveState.CanWave)
			_waitInput = StartCoroutine (WaitInput ());

		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && WaveState != WaveState.IsWaving)
		{
			if(_waitInput != null)
				StopCoroutine (_waitInput);

			if(WaveState != WaveState.HasWaved)
				LaunchRocket ();

			WaveState = WaveState.CanWave;
		}

		else if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended && WaveState == WaveState.IsWaving)
		{
			if(_waitInput != null)
				StopCoroutine (_waitInput);

			Wave ();
			WaveState = WaveState.CanWave;
		}

		#else
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
		#endif

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
		_waveForce = WaveForceLimits.x;

		DOTween.To (()=> _waveForce, x => _waveForce = x, WaveForceLimits.y, MaxForceDuration)
			.SetEase (Ease.OutQuad)
			.SetUpdate (true)
			.SetId ("Wave")
			.OnUpdate (SetCrossHairDistance)
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
		//VibrationManager.Instance.Vibrate (FeedbackType.Jump);

		Instantiate (WaveFX, transform.position, Quaternion.identity);

		_rigidbody.AddForce (recoilDirection * _waveForce, ForceMode.Impulse);
		_waveForce = 0;

		//JumpState = JumpState.InAir;

		if (OnJump != null)
			OnJump ();

		_slowMotion.StopSlowMotion ();

		WaveState = WaveState.HasWaved;

		MasterAudio.PlaySoundAndForget (WaveSound);
	}

	void SetCrossHairDistance ()
	{
		float waveForceValue = (_waveForce - WaveForceLimits.x) / (WaveForceLimits.y - WaveForceLimits.x);
		_crossHairDistance = waveForceValue * _crossHairMax + _crossHairMin;
	}

	void SetCrossHair ()
	{
		Vector3 direction = transform.position - _mainCamera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -_mainCamera.transform.position.z));
		Crosshairs.position = transform.position + direction.normalized * _crossHairDistance;
		Crosshairs.LookAt (transform.position);

		CrossHairRenderer.SetPosition (0, transform.position + direction.normalized * 0.2f);
		CrossHairRenderer.SetPosition (1, Crosshairs.position);

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
		{
			Debug.LogWarning ("No Rocket!");
			return;
		}

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

	public void Grounded ()
	{
		if(OnGrounded != null)
			OnGrounded ();
		
		JumpState = JumpState.Grounded;
	}

	public void InAir ()
	{
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

	public void SetRocket (GameObject rocket)
	{
		CurrentRocket = rocket;

		if (OnRocketChange != null)
			OnRocketChange ();
	}
}
