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

	void Start ()
	{
		_parallaxCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ParallaxCamera> ();

		//transform.DOLocalMoveY (finaleYPos, duration);
	}

	void Update ()
	{
		transform.Translate (Vector3.forward * Speed * _parallaxCamera.delta * Time.deltaTime);
	}
}
