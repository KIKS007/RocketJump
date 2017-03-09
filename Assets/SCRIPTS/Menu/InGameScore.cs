using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGameScore : MonoBehaviour 
{
	public RectTransform right;
	public RectTransform left;
	public float bounceForce = 3;

	private Player _playerScript;
	private RectTransform rect;

	// Use this for initialization
	void Start () 
	{
		rect = GetComponent<RectTransform> ();
		GameManager.Instance.OnPlaying += Setup;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ScreenShakeCamera> ().OnScreenShake += Shake;
	}

	void Setup ()
	{
		_playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		_playerScript.OnJump += Bounce;
	}

	void Bounce ()
	{
		right.DOPunchPosition (new Vector3(0.01f, 1 * bounceForce, 0), 0.3f);
		left.DOPunchPosition (new Vector3(0.01f, 1 * bounceForce, 0), 0.3f);
	}

	void Shake ()
	{
		rect.DOShakePosition (0.2f, new Vector3(10, 2, 0));
	}
}
