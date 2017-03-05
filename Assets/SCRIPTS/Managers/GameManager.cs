﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;

public enum GameState { Menu, Playing, GameOver };

public class GameManager : Singleton<GameManager> 
{
	public GameState GameState = GameState.Playing;
	public string GameScene ="Kiki";

	[Header ("Sounds")]
	[SoundGroup]
	public string MenuGameOver;

	public event EventHandler OnPlaying;
	public event EventHandler OnMenu;
	public event EventHandler OnGameOver;

	public bool FirstLaunch = false;

	void Awake ()
	{
        // +++Amplitude+++ //
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("f5d77f52f038bf0224c9a9ac9d81b0d8");
        // +++Amplitude+++ //

		if(SceneManager.GetSceneByName (GameScene).isLoaded)
			SceneManager.UnloadSceneAsync (GameScene);

		if (GameState == GameState.Menu)
		{
			
			UI.Instance.ShowMaineMenu ();
		}
		else
		{
			StartCoroutine (LoadGame ());
			UI.Instance.HideAll ();
		}

		GameState = GameState.Menu;

        CheckFirstLaunch();

		if (GameState == GameState.Menu)
		{
			
			UI.Instance.ShowMaineMenu ();
		}
		else
		{
			StartCoroutine (LoadGame ());
			UI.Instance.HideAll ();
		}

		GameState = GameState.Menu;

		StartCoroutine (GameStateChange (GameState));

		OnGameOver += () => 
		{
			if(FirstLaunch)
			{
				FirstLaunch = false; 
				PlayerPrefs.SetInt ("FirstLaunch", 0);
			}
		};
	}

	void CheckFirstLaunch ()
	{
		if (PlayerPrefs.GetInt ("FirstLaunch") == 0)
			FirstLaunch = false;
		else
			FirstLaunch = true;
	}

	public void GameOver ()
	{
		StartCoroutine (GameOverCoroutine ());
	}

	IEnumerator GameOverCoroutine ()
	{
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ScreenShakeCamera> ().CameraShaking (FeedbackType.Death);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SlowMotion> ().StartSlowMotion ();

		MixtapesManager.Instance.StartCoroutine ("GameOver");
		MasterAudio.PlaySoundAndForget (MenuGameOver);

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Instantiate (player.GetComponent<Player> ().deathParticle, player.transform.position, Quaternion.identity);
		Destroy (player);

		yield return new WaitForSecondsRealtime (0.5f);

		GameState = GameState.GameOver;
		UI.Instance.ShowGameOver ();

		if(SceneManager.GetSceneByName (GameScene).isLoaded)
			yield return SceneManager.UnloadSceneAsync (GameScene);
	}

	IEnumerator LoadGame ()
	{
		GameState = GameState.Menu;

		if(SceneManager.GetSceneByName (GameScene).isLoaded)
			yield return SceneManager.UnloadSceneAsync (GameScene);
		
		yield return SceneManager.LoadSceneAsync (GameScene, LoadSceneMode.Additive);

		GameState = GameState.Playing;
	}

	IEnumerator GameStateChange (GameState state, bool firstLaunch = false)
	{
		if(!firstLaunch)
		{
			switch (state)
			{
			case GameState.Menu:
				if (OnMenu != null)
					OnMenu ();
				break;
			case GameState.Playing:
				if (OnPlaying != null)
					OnPlaying ();
				break;
			case GameState.GameOver:
				if (OnGameOver != null)
					OnGameOver ();
				break;
			}
		}

		yield return new WaitWhile (() => GameState == state);

		StartCoroutine (GameStateChange (GameState));
	}
}
