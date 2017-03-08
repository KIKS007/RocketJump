using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DarkTonic.MasterAudio;

public class SettingsMenu : MonoBehaviour 
{
	public Slider SFXSlider;
	public Slider MUSICSlider;

	private float _intitialVolumeSFX;
	private float _intitialVolumeMUSIC;

	// Use this for initialization
	void Awake () 
	{
		_intitialVolumeSFX = MasterAudio.MasterVolumeLevel;
		_intitialVolumeMUSIC = MasterAudio.PlaylistMasterVolume;

		if (PlayerPrefs.HasKey ("SFXVolume"))
			SFXSlider.value = PlayerPrefs.GetFloat ("SFXVolume");

		if (PlayerPrefs.HasKey ("MusicVolume"))
			MUSICSlider.value = PlayerPrefs.GetFloat ("MusicVolume");

		SFXChange ();
		MusicChange ();
	}

	public void SFXChange ()
	{
		MasterAudio.MasterVolumeLevel = SFXSlider.value * _intitialVolumeSFX;

		PlayerPrefs.SetFloat ("SFXVolume", SFXSlider.value);
	}

	public void MusicChange ()
	{
		MasterAudio.PlaylistMasterVolume = MUSICSlider.value * _intitialVolumeMUSIC;

		PlayerPrefs.SetFloat ("MusicVolume", MUSICSlider.value);
	}

	public void ClearScore ()
	{
		ScoreManager.Instance.ClearScore ();
	}
}
