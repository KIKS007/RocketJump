using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DarkTonic.MasterAudio;

public enum GameState { Menu, Playing, GameOver, Testing };

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

	[HideInInspector]
	public GameState _initialState;

	void Awake ()
	{
		_initialState = GameState;

        // +++Amplitude+++ //
        Amplitude amplitude = Amplitude.Instance;
        amplitude.logging = true;
        amplitude.init("f5d77f52f038bf0224c9a9ac9d81b0d8");
        // +++Amplitude+++ //

		if (GameState == GameState.Menu)
			UI.Instance.ShowMaineMenu ();

		if(GameState == GameState.Playing)
		{
			StartCoroutine (LoadGame ());
			UI.Instance.HideAll ();
		}

		if(GameState == GameState.Testing)
		{
			StartCoroutine (ReLoadGame ());
			UI.Instance.HideAll ();
		}

        CheckFirstLaunch();

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

	void Start ()
	{
		if(SceneManager.GetSceneByName (GameScene).isLoaded && GameState == GameState.Menu)
			SceneManager.UnloadSceneAsync (GameScene);
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
		GameState = GameState.GameOver;

		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ScreenShakeCamera> ().CameraShaking (FeedbackType.Death);
		VibrationManager.Instance.Vibrate (FeedbackType.Death);
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SlowMotion> ().StartSlowMotion ();

		MixtapesManager.Instance.StartCoroutine ("GameOver");
		MasterAudio.PlaySoundAndForget (MenuGameOver);

		GameObject player = GameObject.FindGameObjectWithTag ("Player");

		Instantiate (player.GetComponent<Player> ().deathParticle, player.transform.position, Quaternion.identity);
		player.SetActive (false);

		yield return new WaitForSecondsRealtime (0.5f);

		if (_initialState != GameState.Testing) 
		{
			UI.Instance.ShowGameOver ();
			GameState = GameState.Menu;
			
			if (SceneManager.GetSceneByName (GameScene).isLoaded)
				yield return SceneManager.UnloadSceneAsync (GameScene);
		} 
		else
			StartCoroutine (ReLoadGame ());
	}

	IEnumerator LoadGame ()
	{
		GameState = GameState.Menu;

		if(SceneManager.sceneCount > 1)
			for(int i = 1; i < SceneManager.sceneCount; i++)
				yield return SceneManager.UnloadSceneAsync (SceneManager.GetSceneAt (i).name);
		
		if(SceneManager.GetSceneByName (GameScene).isLoaded)
			yield return SceneManager.UnloadSceneAsync (GameScene);
		
		yield return SceneManager.LoadSceneAsync (GameScene, LoadSceneMode.Additive);

		GameState = GameState.Playing;
	}

	IEnumerator ReLoadGame ()
	{
		GameState = GameState.Menu;

		if(SceneManager.sceneCount == 1)
			Debug.LogWarning ("There's only ONE scene!");

		string scene = SceneManager.GetSceneAt (1).name;

		if(scene == "Menu")
			Debug.LogWarning ("Menu isn't The Active Scene!");

		yield return SceneManager.UnloadSceneAsync (scene);
		yield return SceneManager.LoadSceneAsync (scene, LoadSceneMode.Additive);

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
