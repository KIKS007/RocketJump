using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI : MonoBehaviour {
    public GameObject PanelCredit;
    public GameObject PanelHowToPlay;
    public Button VolumeOff;
    public Button VolumeOn;
    public bool Sound;

    public GameManager GameManager;
	// Use this for initialization
	void Start () {
       
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPanelCredit ()
    {
        PanelCredit.SetActive(true);
    }

    public void ShowPanelHowToPlay ()
    {
        PanelHowToPlay.SetActive(true);

    }

    public void ChangeVolume ()
    {
        Sound = !Sound;
            if(Sound == true)
        {
            VolumeOff.gameObject.SetActive(true);
            VolumeOn.gameObject.SetActive(false);
            AudioListener.volume = 0;

        }
            if(Sound == false)
        {
            VolumeOff.gameObject.SetActive(false);
            VolumeOn.gameObject.SetActive(true);
            AudioListener.volume = 1;
        }

    }

    public void StartGame ()
    {
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {

    }

    
}
