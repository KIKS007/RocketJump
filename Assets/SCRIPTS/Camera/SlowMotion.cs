using UnityEngine;
using System.Collections;
using DG.Tweening;

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

	private float _initialTimeScale;
	private float _initialFixedDelta;

	void Awake ()
	{
		Time.timeScale = 1f;

		_initialTimeScale = Time.timeScale;
		_initialFixedDelta = Time.fixedDeltaTime;
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
		DOTween.Kill ("StopSlowMotion");

		if (OnSlowMotionStart != null)
			OnSlowMotionStart ();

		DOTween.To(()=> Time.timeScale, x=> Time.timeScale =x, _initialTimeScale/SlowFactor, TimeTween).SetEase(easetype).SetId("StartSlowMotion");
		DOTween.To(()=> Time.fixedDeltaTime, x=> Time.fixedDeltaTime =x, _initialFixedDelta/SlowFactor, TimeTween).SetEase(easetype).SetId("StartSlowMotion");
	}

	public void StopSlowMotion ()
	{
		//Debug.Log("Undo Slomo !");

		if (OnSlowMotionStop != null)
			OnSlowMotionStop ();

		DOTween.To(()=> Time.timeScale, x=> Time.timeScale =x, 1, TimeTween).SetEase(easetype).SetId("StopSlowMotion");
		DOTween.To(()=> Time.fixedDeltaTime, x=> Time.fixedDeltaTime =x, _initialFixedDelta, TimeTween).SetEase(easetype).SetId("StopSlowMotion");
	}
}