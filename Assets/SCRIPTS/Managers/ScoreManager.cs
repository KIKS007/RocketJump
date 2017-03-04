using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager> 
{
	public List<int> BestScores = new List<int>();
	public int BestScoreLimit = 5;
	public int CurrentScore;
	public float InitialPosition = -4;

	[Header ("Score Text")]
	public Text ScoreText;

	[Header ("Menu Score Text")]
	public Text MenuScoreText;
	public Text MenuBestScoreText;

	[Header ("Scores")]
	public int ClimbingScore;
	public int EnemyScore;
	public int PickupScore;

	[Header ("Scores Factor")]
	public float ClimbingScoreFactor = 1;
	public float EnemyScoreFactor = 1;
	public float PickupScoreFactor = 1;

	private Transform _mainCamera;
	private GameObject _scoreCanvas;
	// Use this for initialization
	void Start () 
	{
		_mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		_scoreCanvas = transform.GetChild (0).gameObject;
		BestScores.Sort (new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

		GameManager.Instance.OnGameOver += OnGameOver;

		GameManager.Instance.OnGameOver += ()=> transform.GetChild (0).gameObject.SetActive (false);
		GameManager.Instance.OnPlaying += ()=> transform.GetChild (0).gameObject.SetActive (true);

		transform.GetChild (0).gameObject.SetActive (false);

		GetSavedScores ();
	}

	void GetSavedScores ()
	{
		BestScores.Clear ();

		for (int i = 0; i < BestScoreLimit; i++)
			BestScores.Add (PlayerPrefs.GetInt ("BestScore" + i));
	}

	void SaveScores ()
	{
		for (int i = 0; i < BestScores.Count; i++)
			PlayerPrefs.SetInt ("BestScore" + i, BestScores [i]);
	}
	
	// Update is called once per frame
	void Update () 
	{
		GetClimbingScore ();

		CurrentScore = ClimbingScore + EnemyScore + PickupScore;
		ScoreText.text = "Score: " + CurrentScore.ToString ();
	}

	void GetClimbingScore ()
	{
		ClimbingScore = (int)(ClimbingScoreFactor * (_mainCamera.transform.position.y - InitialPosition));

		if (ClimbingScore < 0)
			ClimbingScore = 0;
	}

	public void EnemyKilled (int score)
	{
		EnemyScore += (int)(score * EnemyScoreFactor);
	}

	public void PickupCollected (int score)
	{
		PickupScoreFactor += (int)(score * PickupScoreFactor);
	}

	void OnGameOver ()
	{
		BestScores.Add (CurrentScore);
		CurrentScore = 0;


		BestScores.Sort (new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

		if (BestScores.Count > BestScoreLimit)
			BestScores.RemoveAt (BestScores.Count - 1);

		MenuScoreText.text = CurrentScore.ToString ();
		MenuBestScoreText.text = BestScores [0].ToString ();

		SaveScores ();
	}
}
