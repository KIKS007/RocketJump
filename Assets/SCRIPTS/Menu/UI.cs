using System.Collections;
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
    public GameObject PanelHowToPlay2;
	public GameObject PanelMixtape;

	[Header ("Game Over")]
	public GameObject PanelGameOver;

	private bool isLoading = false;

	// Use this for initialization
	void Start () 
	{
		DisableAll ();
		ShowMaineMenu ();

		GameManager.Instance.OnPlaying += ()=> PanelMixtape.SetActive (true);
		GameManager.Instance.OnPlaying += ()=> isLoading = false;
		GameManager.Instance.OnGameOver += ()=> PanelMixtape.SetActive (false);
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
		PanelHowToPlay2.SetActive(false);
		PanelMixtape.SetActive(false);
		PanelGameOver.SetActive(false);
		PanelMainMenu.SetActive(false);
	}

    public void ShowPanelCredit ()
    {
        PanelCredit.SetActive(true);
        PanelMainMenu.SetActive(false);
        DoomBoxMesh.SetActive(false);
    }

    public void ShowPanelHowToPlay ()
    {
        PanelHowToPlay.SetActive(true);
        PanelMainMenu.SetActive(false);
        DoomBoxMesh.SetActive(false);
    }

    public void ShowPanelHowToPlay2 ()
    {
        PanelHowToPlay2.SetActive(true);
        PanelHowToPlay.SetActive(false);
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
		if(!isLoading)
			StartCoroutine (WaitLoadScene ());
    }

	IEnumerator WaitLoadScene ()
	{
		isLoading = true;

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
