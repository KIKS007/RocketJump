using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Menu, Playing, GameOver };

public class GameManager : Singleton<GameManager> 
{
	public GameState GameState = GameState.Playing;

	public void GameOver ()
	{
		StartCoroutine (LoadScene ());
	}

	IEnumerator LoadScene ()
	{
		GameState = GameState.GameOver;

		yield return SceneManager.LoadSceneAsync (SceneManager.GetActiveScene ().name);

		GameState = GameState.Playing;
	}
}
