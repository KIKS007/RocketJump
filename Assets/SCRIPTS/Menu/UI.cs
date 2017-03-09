using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DarkTonic.MasterAudio;
using DG.Tweening;

public class UI : Singleton<UI> 
{
	[Header ("Navigation")]
	public float MenuMovementDuration;
	public Ease MenuMovementEase;

	[Header ("Panels")]
	public RectTransform[] AllPanels = new RectTransform[0];

	[Header ("Panels")]
	public Transform[] Backgrounds = new Transform[0];

	[Header ("Icons")]
	public float IconDuration = 0.5f;
	public RectTransform[] Icons = new RectTransform[5];
	public RectTransform SelectionBox;

	[Header ("Sounds")]
	[SoundGroup]
	public string MenuCancel;

	private bool isLoading = false;

	private float initialScale = 0.85f;

	// Use this for initialization
	void Start () 
	{
		AllPanels [0].parent.gameObject.SetActive (true);
		GameManager.Instance.OnPlaying += ()=> isLoading = false;

		if(GameManager.Instance._initialState == GameState.Menu)
		{
			EnableAll ();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if(GameManager.Instance.GameState != GameState.Playing)
		{
			if(Input.GetKey(KeyCode.Escape))
			{
				ShowMenu (2);

				MasterAudio.PlaySoundAndForget (MenuCancel);
			}
		}
			
	}

	public void ShowMenu (int whichMenu)
	{
		DOTween.Kill ("MenuMovement");

		float difference = -AllPanels [whichMenu].anchoredPosition.x;

		foreach(RectTransform rect in AllPanels)
			rect.DOAnchorPosX (rect.anchoredPosition.x + difference, MenuMovementDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");

		SelectionBox.DOAnchorPosX (Icons [whichMenu].anchoredPosition.x, IconDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");;

		foreach(RectTransform rect in Icons)
			rect.DOScale (initialScale, IconDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");

		Icons [whichMenu].DOScale (1, IconDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");
	}

	public void ShowInstantMenu (int whichMenu)
	{
		DOTween.Kill ("MenuMovement");

		float difference = -AllPanels [whichMenu].anchoredPosition.x;

		foreach(RectTransform rect in AllPanels)
			rect.anchoredPosition = new Vector2 (rect.anchoredPosition.x + difference, rect.anchoredPosition.y);

		SelectionBox.DOAnchorPosX (Icons [whichMenu].anchoredPosition.x, IconDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");

		foreach(RectTransform rect in Icons)
			rect.DOScale (initialScale, IconDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");
		
		Icons [whichMenu].DOScale (1, IconDuration).SetEase (MenuMovementEase).SetId ("MenuMovement");
	}

	public void DisableAll ()
	{
		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (false);

		foreach (RectTransform rect in AllPanels)
			rect.gameObject.SetActive (false);
	}

	public void EnableAll ()
	{
		foreach (Transform back in Backgrounds)
			back.gameObject.SetActive (true);

		foreach (RectTransform rect in AllPanels)
			rect.gameObject.SetActive (true);
	}
		
	public void ShowGameOver ()
	{
		ShowInstantMenu (4);

		EnableAll ();
	}

	public void ShowMainMenu ()
	{
		ShowInstantMenu (2);

		EnableAll ();
	}

    public void StartGame ()
    {
		if(!isLoading)
			StartCoroutine (WaitLoadScene ("LoadGame"));
    }

	public void StartTuto ()
	{
		if(!isLoading)
			StartCoroutine (WaitLoadScene ("LoadTuto"));
	}

	IEnumerator WaitLoadScene (string coroutine)
	{
		isLoading = true;

		yield return GameManager.Instance.StartCoroutine (coroutine);

		DisableAll ();
	}

    public void QuitGame()
    {
		Application.Quit ();
    }

    
}
