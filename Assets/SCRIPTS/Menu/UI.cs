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
	//public GameObject DoomBoxMesh;

	[Header ("Panels")]
	public GameObject PanelCredit;
    public GameObject PanelSettings;
	public GameObject PanelHowToPlay;
    public GameObject PanelHowToPlay2;
	//public GameObject PanelMixtape;

	[Header ("Panels")]
	public Transform[] Backgrounds = new Transform[0];

	[Header ("Game Over")]
	public GameObject PanelGameOver;

	[Header ("Sounds")]
	[SoundGroup]
	public string MenuCancel;

	private bool isLoading = false;

	// Use this for initialization
	void Start () 
	{
		//GameManager.Instance.OnPlaying += ()=> PanelMixtape.SetActive (true);
		GameManager.Instance.OnPlaying += ()=> isLoading = false;
		//GameManager.Instance.OnGameOver += ()=> PanelMixtape.SetActive (false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.GameState != GameState.Playing)
			if(Input.GetKey(KeyCode.Escape))
			{
				ShowMainMenu ();
				
				MasterAudio.PlaySoundAndForget (MenuCancel);
			}
	}

	void DisableAll ()
	{
		PanelCredit.SetActive(false);
		PanelHowToPlay.SetActive(false);
		PanelHowToPlay2.SetActive(false);
		PanelSettings.SetActive(false);
		//PanelMixtape.SetActive(false);
		PanelGameOver.SetActive(false);
		PanelMainMenu.SetActive(false);
	}

	public void ShowMainMenu ()
	{
		DisableAll ();

		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (true);

		MenuMesh.SetActive(true);
		//DoomBoxMesh.SetActive (true);
		PanelMainMenu.SetActive(true);
	}

    public void ShowPanelCredit ()
    {
		DisableAll ();
        
		PanelCredit.SetActive(true);
    }

	public void ShowPanelSettings ()
	{
		DisableAll ();

		PanelSettings.SetActive(true);
	}

    public void ShowPanelHowToPlay ()
    {
		DisableAll ();
        
		PanelHowToPlay.SetActive(true);
    }

    public void ShowPanelHowToPlay2 ()
    {
		DisableAll ();
       
		PanelHowToPlay2.SetActive(true);
    }

	public void ShowGameOver ()
	{
		DisableAll ();

		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (true);

		PanelGameOver.SetActive(true);
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

		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (false);

		MenuMesh.SetActive(false);
		//DoomBoxMesh.SetActive (false);
		
	}

    public void QuitGame()
    {
		Application.Quit ();
    }

    
}
