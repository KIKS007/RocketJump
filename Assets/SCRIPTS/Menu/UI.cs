﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DarkTonic.MasterAudio;
using DG.Tweening;

public class UI : Singleton<UI> 
{
	[Header ("Navigation")]
	public float MenuMovementDuration;
	public Ease MenuMovementEase;

	[Header ("Panels")]
	public RectTransform[] AllPanels = new RectTransform[0];

	[Header ("Panels")]
	public Transform[] Backgrounds = new Transform[0];

	[Header ("Sounds")]
	[SoundGroup]
	public string MenuCancel;

	private bool isLoading = false;

	// Use this for initialization
	void Start () 
	{
		GameManager.Instance.OnPlaying += ()=> isLoading = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.GameState != GameState.Playing)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				ShowMenu (2);

				MasterAudio.PlaySoundAndForget (MenuCancel);
			}
		}
			
	}

	public void ShowMenu (int whichMenu)
	{
		DOTween.Kill ("MenuMovement");

		float difference = -AllPanels [whichMenu].anchoredPosition.x;

		foreach(RectTransform rect in AllPanels)
			rect.DOAnchorPosX (rect.anchoredPosition.x + difference, MenuMovementDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");
	}

	public void ShowInstantMenu (int whichMenu)
	{
		DOTween.Kill ("MenuMovement");

		float difference = -AllPanels [whichMenu].anchoredPosition.x;

		foreach(RectTransform rect in AllPanels)
			rect.anchoredPosition = new Vector2 (rect.anchoredPosition.x + difference, rect.anchoredPosition.y);
	}

	public void DisableAll ()
	{
		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (false);

		foreach (RectTransform rect in AllPanels)
			rect.gameObject.SetActive (false);
	}

	public void EnableAll ()
	{
		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (true);

		foreach (RectTransform rect in AllPanels)
			rect.gameObject.SetActive (true);
	}
		
	public void ShowGameOver ()
	{
		ShowInstantMenu (3);

		EnableAll ();
	}

    public void StartGame ()
    {
		if(!isLoading)
			StartCoroutine (WaitLoadScene ());
    }

	IEnumerator WaitLoadScene ()
	{
		isLoading = true;

		yield return GameManager.Instance.StartCoroutine ("LoadGame");

		DisableAll ();
	}

    public void QuitGame()
    {
		Application.Quit ();
    }

    
}
