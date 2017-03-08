using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour 
{
	public delegate void ParallaxCameraDelegate(float deltaMovement);
	public ParallaxCameraDelegate onCameraTranslate;
	private float oldPosition;

	public float delta;

	void Start()
	{
		oldPosition = transform.position.y;

		GameManager.Instance.OnPlaying += ()=> oldPosition = transform.position.y;

	}

	void Update()
	{
		delta = transform.position.y - oldPosition;

		if (onCameraTranslate != null)
			onCameraTranslate(delta);
		
		oldPosition = transform.position.y;
	}
}
