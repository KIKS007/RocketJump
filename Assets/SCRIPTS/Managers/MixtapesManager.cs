using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MixtapesManager : Singleton<MixtapesManager> 
{
	[Header ("Mixtapes")]
	public int MixtapeIndex = 0;
	public Wave CurrentWave;
	public Wave[] SelectedMixtapes = new Wave[3];

	[Header ("Unlocked Mixtapes")]
	public List<Wave> UnlockedMixtapes = new List<Wave>();

	[Header ("Mixtape State")]
	public bool IsPaused = false;

	[Header ("Music Fade")]
	public float MusicFadeTime = 0.5f;

	private Player _playerScript;
	private string _previousMusic;

	public event EventHandler OnMixtapeChange;

	void Awake ()
	{
		SetupPlaylist ();
		SceneManager.sceneLoaded += (arg0, arg1) => SetupPlaylist();

		if(GameManager.Instance.GameState == GameState.Menu)
			MasterAudio.FadeSoundGroupToVolume (SelectedMixtapes [Random.Range(0, SelectedMixtapes.Length)].Music, 1, MusicFadeTime);

		GameManager.Instance.OnPlaying += FirstMixtape; 
	}

	void SetupPlaylist ()
	{
		foreach (Wave wave in SelectedMixtapes)
		{
			MasterAudio.PlaySoundAndForget (wave.Music, 1);
			MasterAudio.FadeSoundGroupToVolume (wave.Music, 0, 0);
		}
	}

	void FirstMixtape ()
	{
		foreach (Wave wave in SelectedMixtapes)
			MasterAudio.FadeSoundGroupToVolume (wave.Music, 0, 0);

		_playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		_playerScript.SetWave (SelectedMixtapes [MixtapeIndex]);
		CurrentWave = SelectedMixtapes [MixtapeIndex];
		SetPlaylist ();
		StartCoroutine (MixtapeDuration ());
	}

	public void MixtapeUnlocked (Wave wave)
	{
		if (!UnlockedMixtapes.Contains (wave))
			UnlockedMixtapes.Add (wave);
		else
			Debug.LogWarning ("Mixtape Already Unlocked!");
	}

	IEnumerator MixtapeDuration ()
	{
		if (OnMixtapeChange != null)
			OnMixtapeChange ();

		float duration = 0;

		while(!IsPaused && duration < CurrentWave.MixtapeDuration)
		{
			duration += Time.unscaledDeltaTime;

			yield return new WaitForEndOfFrame ();
		}

		if(GameManager.Instance.GameState == GameState.Playing)
			NextMixtape ();
	}

	public void NextMixtape ()
	{
		StopCoroutine (MixtapeDuration ());

		MixtapeIndex++;

		if (MixtapeIndex == 3)
			MixtapeIndex = 0;

		_playerScript.SetWave (SelectedMixtapes [MixtapeIndex]);
		CurrentWave = SelectedMixtapes [MixtapeIndex];

		SetPlaylist ();
		StartCoroutine (MixtapeDuration ());
	}

	public void PreviousMixtape ()
	{
		StopCoroutine (MixtapeDuration ());

		MixtapeIndex--;

		if (MixtapeIndex == -1)
			MixtapeIndex = 2;

		_playerScript.SetWave (SelectedMixtapes [MixtapeIndex]);
		CurrentWave = SelectedMixtapes [MixtapeIndex];

		SetPlaylist ();
		StartCoroutine (MixtapeDuration ());
	}

	void SetPlaylist ()
	{
		if(_previousMusic != null)
			MasterAudio.FadeSoundGroupToVolume (_previousMusic, 0, MusicFadeTime);

		MasterAudio.FadeSoundGroupToVolume (SelectedMixtapes [MixtapeIndex].Music, 1, MusicFadeTime);
		_previousMusic = SelectedMixtapes [MixtapeIndex].Music;
	}

	public void PauseMixtape (float duration)
	{
		StartCoroutine (PauseDuration (duration));
	}

	IEnumerator PauseDuration (float duration)
	{
		IsPaused = true;

		yield return new WaitForSecondsRealtime (duration);

		IsPaused = false;
	}

	public void FadeAll (float fadeDuration = 0.2f)
	{
		foreach (Wave wave in SelectedMixtapes)
			MasterAudio.FadeSoundGroupToVolume (wave.Music, 0, fadeDuration);
	}

	public IEnumerator GameOver ()
	{
		FadeAll ();
		MasterAudio.StopEverything ();

		yield return new WaitForSeconds (1.5f);

		if (GameManager.Instance.GameState != GameState.Playing)
		{
			if(GameManager.Instance.GameState == GameState.Menu)
				MasterAudio.FadeSoundGroupToVolume (SelectedMixtapes [Random.Range(0, SelectedMixtapes.Length)].Music, 1, MusicFadeTime);
		}
	}
}
