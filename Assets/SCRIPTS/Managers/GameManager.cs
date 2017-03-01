﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> 
{
	public void GameOver ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
