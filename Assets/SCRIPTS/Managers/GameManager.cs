using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Menu, Playing, GameOver };

public class GameManager : Singleton<GameManager> 
{
	public GameState GameState = GameState.Playing;

	public event EventHandler OnPlaying;
	public event EventHandler OnMenu;
	public event EventHandler OnGameOver;
    public GameObject PanelGameOver;

	void Start ()
	{
		StartCoroutine (GameStateChange (GameState));

		if(GameState == GameState.Menu)
			MenuManager.Instance.ShowMenu (MenuManager.Instance.mainMenu.GetComponent<MenuComponent> ());
	}

	public void GameOver ()
	{
        ShowPanelGameOver();

		//if(GameState != GameState.GameOver)
			//StartCoroutine (LoadScene ());
	}

    void ShowPanelGameOver ()
    {
        PanelGameOver.SetActive(true);
    }

	IEnumerator LoadScene ()
	{
		GameState = GameState.GameOver;

		string scene = SceneManager.GetSceneAt (1).name;

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
