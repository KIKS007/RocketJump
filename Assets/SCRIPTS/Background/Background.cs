using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Background : MonoBehaviour 
{
	public float Speed;
	private ParallaxCamera _parallaxCamera;

	/*public float duration;
	public float finaleYPos;*/

	private Vector3 _initialPos;

	void Start ()
	{
		_initialPos = transform.localPosition;
		_parallaxCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ParallaxCamera> ();

		GameManager.Instance.OnPlaying += () => transform.localPosition = _initialPos;

		//transform.DOLocalMoveY (finaleYPos, duration);
	}

	void Update ()
	{
		if(GameManager.Instance.GameState == GameState.Playing)
			transform.Translate (Vector3.down * Speed * _parallaxCamera.delta * Time.deltaTime);
	}
}
