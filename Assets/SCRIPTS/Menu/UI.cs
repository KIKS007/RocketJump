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
        Sound = !Sound;
            if(Sound == true)
        {
            VolumeOff.gameObject.SetActive(true);
            VolumeOn.gameObject.SetActive(false);

			AudioListener.volume = 0;
			MasterAudio.MasterVolumeLevel = 0;

        }
            if(Sound == false)
        {
            VolumeOff.gameObject.SetActive(false);
            VolumeOn.gameObject.SetActive(true);

			MasterAudio.MasterVolumeLevel = 1;
        }

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
