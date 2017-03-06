using UnityEngine;
using System.Collections;
using DG.Tweening;
using Colorful;

public class SlowMotion : MonoBehaviour 
{
	public event EventHandler OnSlowMotionStart;
	public event EventHandler OnSlowMotionStop;

	public Ease easetype;

	[Header ("SlowMotion InGame")]
	public float SlowFactor;
	public float TimeTween;

	[Header ("Debug Test")]
	public bool SlowMotionDebug;
	public float TimeScaleDebug;

	[Header ("Slow Motion Effect")]
	public Renderer slowMoWaves;
	public float effectDuration;
	public float wavesValue;
	public float vignettingValue;

	private FastVignette _vignetting;

	private float _initialTimeScale;
	private float _initialFixedDelta;

	private float _initialVignetting;

	void Awake ()
	{
		Time.timeScale = 1f;

		_initialTimeScale = Time.timeScale;
		_initialFixedDelta = Time.fixedDeltaTime;
		_vignetting = GetComponent<FastVignette> ();
		_initialVignetting = _vignetting.Darkness;

		GameManager.Instance.OnGameOver += StopSlowMotion;
	}

	// Update is called once per frame
	void Update () 
	{
		if(SlowMotionDebug)
		{
			SlowMotionDebug = false;
			StartSlowMotion ();
		}

		TimeScaleDebug = Time.timeScale;
	}

	public void StartSlowMotion ()
	{
		DOTween.Kill ("SlowMotion");

		if (OnSlowMotionStart != null)
			OnSlowMotionStart ();

		DOTween.To(()=> Time.timeScale, x=> Time.timeScale =x, _initialTimeScale/SlowFactor, TimeTween).SetEase(easetype).SetId("SlowMotion");
		DOTween.To(()=> Time.fixedDeltaTime, x=> Time.fixedDeltaTime =x, _initialFixedDelta/SlowFactor, TimeTween).SetEase(easetype).SetId("SlowMotion");

		StartEffect ();
	}

	public void StopSlowMotion ()
	{
		//Debug.Log("Undo Slomo !");

		if (OnSlowMotionStop != null)
			OnSlowMotionStop ();

		DOTween.To(()=> Time.timeScale, x=> Time.timeScale =x, 1, TimeTween).SetEase(easetype).SetId("SlowMotion");
		DOTween.To(()=> Time.fixedDeltaTime, x=> Time.fixedDeltaTime =x, _initialFixedDelta, TimeTween).SetEase(easetype).SetId("SlowMotion");

		StopEffect ();
	}

	void StartEffect ()
	{
		slowMoWaves.material.DOFloat (wavesValue,  "_EffectStrength", effectDuration).SetId("SlowMotion");
		DOTween.To(()=> _vignetting.Darkness, x=> _vignetting.Darkness =x, vignettingValue, effectDuration).SetId("SlowMotion");
	}

	void StopEffect ()
	{
		slowMoWaves.material.DOFloat (0,  "_EffectStrength", effectDuration).SetId("SlowMotion");;
		DOTween.To(()=> _vignetting.Darkness, x=> _vignetting.Darkness =x, _initialVignetting, effectDuration).SetId("SlowMotion");
	}
}