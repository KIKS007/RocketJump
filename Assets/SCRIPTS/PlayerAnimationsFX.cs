﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsFX : MonoBehaviour 
{
	public Animator PlayerAnimator;

	private Player _playerScript;

	// Use this for initialization
	void Start () 
	{
		_playerScript = GetComponent<Player> ();
		_playerScript.OnJump += OnJump;
		_playerScript.OnGrounded += OnGrounded;
	}

	void OnJump ()
	{
		int newKeyPose = 0;

		do 
		{
			newKeyPose = Random.Range (0, 9);
		}
		while (PlayerAnimator.GetInteger ("Keypose") == newKeyPose);

		/*Debug.Log ("-----------");
		Debug.Log (newKeyPose);
		Debug.Log (PlayerAnimator.GetInteger ("Keypose"));*/

		PlayerAnimator.SetInteger ("Keypose", newKeyPose);
		PlayerAnimator.SetTrigger ("Rocket");
	}

	void OnGrounded ()
	{
		PlayerAnimator.SetTrigger ("Idle");
	}
}
