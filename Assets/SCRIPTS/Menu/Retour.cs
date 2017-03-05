using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class Retour : MonoBehaviour {

	public GameObject PanelCredit;
	public GameObject PanelHowToplay;
	public GameObject PanelHowToplay2;
	public GameObject PanelMainMenu;
	public GameObject DoomBoxMeshes;


	[Header ("Sounds")]
	[SoundGroup]
	public string MenuCancel;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(GameManager.Instance.GameState != GameState.Playing)
		if(Input.GetKey(KeyCode.Escape))
		{
			PanelCredit.SetActive(false);
			PanelHowToplay.SetActive(false);
			PanelHowToplay2.SetActive(false);
			PanelMainMenu.SetActive(true);
			DoomBoxMeshes.SetActive(true);

			MasterAudio.PlaySoundAndForget (MenuCancel);
		}

	}
}