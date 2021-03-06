﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DarkTonic.MasterAudio;

public class UI : Singleton<UI> 
{
	public GameObject MenuMesh;

	[Header ("Main Menu")]
	public GameObject PanelMainMenu;
	public GameObject DoomBoxMesh;

	[Header ("Panels")]
    public GameObject PanelCredit;
	public GameObject PanelHowToPlay;
	public GameObject PanelMixtape;

	[Header ("Game Over")]
	public GameObject PanelGameOver;

	[Header ("Buttons")]
    public Button VolumeOff;
    public Button VolumeOn;
    public bool Sound;

	// Use this for initialization
	void Start () 
	{
       
		
	}

	public void ShowMaineMenu ()
	{
		DisableAll ();
		MenuMesh.SetActive(true);
		DoomBoxMesh.SetActive (true);
		PanelMainMenu.SetActive(true);

	}

	void DisableAll ()
	{
		PanelCredit.SetActive(false);
		PanelHowToPlay.SetActive(false);
		PanelMixtape.SetActive(false);
		PanelGameOver.SetActive(false);
		PanelMainMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			PanelCredit.SetActive(false);
			PanelHowToPlay.SetActive(false);
		}
	}

    public void ShowPanelCredit ()
    {
        PanelCredit.SetActive(true);
    }

    public void ShowPanelHowToPlay ()
    {
        PanelHowToPlay.SetActive(true);

    }

	public void ShowGameOver ()
	{
		PanelGameOver.SetActive(true);
		MenuMesh.SetActive(true);
	}

    public void ChangeVolume ()
    {
		MasterAudio.ToggleMuteBus ("MUSIC");
    }

    public void StartGame ()
    {
		StartCoroutine (WaitLoadScene ());
    }

	IEnumerator WaitLoadScene ()
	{
		yield return GameManager.Instance.StartCoroutine ("LoadGame");

		HideAll ();
	}

	public void HideAll ()
	{
		DisableAll ();
		MenuMesh.SetActive(false);
		DoomBoxMesh.SetActive (false);
		
	}

    public void QuitGame()
    {
		Application.Quit ();
    }

    
}
