using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

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

	[Header ("In Game Score")]
	public RectTransform inGameScore;

	[Header ("Popup Score")]
	public GameObject ScorePopup;

	private Transform _mainCamera;
	private Transform _mainCanvas;

	// Use this for initialization
	void Start () 
	{
		_mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
		_mainCanvas = GameObject.FindGameObjectWithTag ("MenuCanvas").transform;

		BestScores.Sort (new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

		GameManager.Instance.OnGameOver += OnGameOver;

		GameManager.Instance.OnGameOver += ()=> inGameScore.DOAnchorPosY (-155f, 0.5f);
		GameManager.Instance.OnMenu += ()=> inGameScore.DOAnchorPosY (-155f, 0.5f);
		GameManager.Instance.OnPlaying += ()=> inGameScore.DOAnchorPosY (0, 0.5f);

		transform.GetChild (0).gameObject.SetActive (true);
		inGameScore.anchoredPosition = new Vector2 (0, -155);

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

	public void ClearScore ()
	{
		for (int i = 0; i < BestScores.Count; i++)
			PlayerPrefs.SetInt ("BestScore" + i, 0);

		BestScores.Clear ();

		ClimbingScore = 0;
		EnemyScore = 0;
		PickupScore = 0;
		CurrentScore = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(GameManager.Instance.GameState == GameState.Playing)
		{
			GetClimbingScore ();

			CurrentScore = ClimbingScore + EnemyScore + PickupScore;
			ScoreText.text = CurrentScore.ToString ();
		}
	}

	void GetClimbingScore ()
	{
		if(GameManager.Instance.GameState == GameState.Playing)
		{
			if((int)(ClimbingScoreFactor * (_mainCamera.transform.position.y - InitialPosition)) > ClimbingScore)
				ClimbingScore = (int)(ClimbingScoreFactor * (_mainCamera.transform.position.y - InitialPosition));
		}

		if (ClimbingScore < 0)
			ClimbingScore = 0;
	}

	public void EnemyKilled (int score)
	{
		if(GameManager.Instance.GameState == GameState.Playing)
			EnemyScore += (int)(score * EnemyScoreFactor);
	}

	public void PickupCollected (int score)
	{
		if(GameManager.Instance.GameState == GameState.Playing)
			PickupScore += (int)(score * PickupScoreFactor);
	}

	void OnGameOver ()
	{
		BestScores.Add (CurrentScore);

		BestScores.Sort (new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

		if (BestScores.Count > BestScoreLimit)
			BestScores.RemoveAt (BestScores.Count - 1);

		MenuScoreText.text = CurrentScore.ToString ();
		MenuBestScoreText.text = BestScores [0].ToString ();

		ClimbingScore = 0;
		EnemyScore = 0;
		PickupScore = 0;
		CurrentScore = 0;

		SaveScores ();
	}


	public void PopupScore (Transform target, int score, float width = 1.5f)
	{
		GameObject canvas = Instantiate (ScorePopup, target.position, Quaternion.identity) as GameObject;
		canvas.GetComponent<Canvas> ().worldCamera = _mainCamera.GetComponent<Camera> ();
		canvas.transform.GetChild (0).GetComponent<Text> ().text = "+" + score;
		canvas.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta = new Vector2 (width, canvas.transform.GetChild (0).GetComponent<RectTransform> ().sizeDelta.y);

		/*//Vector3 position = _mainCamera.GetComponent<Camera> ().WorldToScreenPoint (target.TransformPoint (target.position));
		Vector3 position = GetScreenPosition (target, _mainCanvas.GetComponent<Canvas> (), _mainCamera.GetComponent<Camera> ());

		GameObject popup = Instantiate (ScorePopup, position, Quaternion.identity, _mainCanvas) as GameObject;
		popup.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (position.x, position.y, 0);
		popup.GetComponent<Text> ().text = "+" + score;*/
	}

	public static Vector3 GetScreenPosition(Transform transform,Canvas canvas,Camera cam)
	{
		Vector3 pos;
		float width = canvas.GetComponent<RectTransform> ().sizeDelta.x;
		float height = canvas.GetComponent<RectTransform > ().sizeDelta.y;
		float x = Camera.main.WorldToScreenPoint (transform.position).x / Screen.width;
		float y = Camera.main.WorldToScreenPoint (transform.position).y / Screen.height;
		pos = new Vector3 (width * x - width / 2, y * height - height / 2); 
		return pos;    
	}
}
