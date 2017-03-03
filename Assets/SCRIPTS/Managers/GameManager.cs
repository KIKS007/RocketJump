using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Menu, Playing, GameOver };

public class GameManager : Singleton<GameManager> 
{
	public GameState GameState = GameState.Playing;
	public string GameScene ="Kiki";

	public event EventHandler OnPlaying;
	public event EventHandler OnMenu;
	public event EventHandler OnGameOver;

	public bool FirstLaunch = false;

	void Start ()
	{
		CheckFirstLaunch ();

		StartCoroutine (GameStateChange (GameState));

		if (GameState == GameState.Menu)
			MenuManager.Instance.ShowMenu (MenuManager.Instance.mainMenu.GetComponent<MenuComponent> ());
		else
			StartCoroutine (LoadScene (GameScene));

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
		if(GameState != GameState.GameOver)
			StartCoroutine (LoadScene ());
	}

	IEnumerator LoadScene ()
	{
		GameState = GameState.GameOver;

		string scene = SceneManager.GetSceneAt (1).name;

		if(SceneManager.GetSceneByName (scene).isLoaded)
			yield return SceneManager.UnloadSceneAsync (scene);
		
		yield return SceneManager.LoadSceneAsync (scene, LoadSceneMode.Additive);

		GameState = GameState.Playing;
	}

	IEnumerator LoadScene (string scene)
	{
		GameState = GameState.Menu;

		if(SceneManager.GetSceneByName (scene).isLoaded)
			yield return SceneManager.UnloadSceneAsync (scene);
		
		yield return SceneManager.LoadSceneAsync (scene, LoadSceneMode.Additive);

		GameState = GameState.Playing;
	}

	IEnumerator GameStateChange (GameState state)
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

		yield return new WaitWhile (() => GameState == state);

		StartCoroutine (GameStateChange (GameState));
	}
}
