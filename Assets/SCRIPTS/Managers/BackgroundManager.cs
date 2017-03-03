using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour 
{
	[Header ("Speed")]
	public float BackgroundSpeed;
	public float MiddlegroundSpeed;
	public float ForegroundSpeed;

	[Header ("Parents")]
	public Transform BackgroundParent;
	public Transform MiddlegroundParent;
	public Transform ForegroundParent;

	[Header ("Settings")]
	public float BackgroundHeight;
	public float MiddlegroundHeight;
	public float ForegroundHeight;

	private Transform _mainCamera;
	private float _positionDelta;
	private float _oldPosition;

	// Use this for initialization
	void Start () 
	{
		_mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		Setup ();
	}

	void Setup ()
	{
		int index = BackgroundParent.childCount - 1;

		foreach(Transform child in BackgroundParent)
		{
			child.localPosition = new Vector3 (0, BackgroundHeight * index, 0);
			index--;
		}

		index = MiddlegroundParent.childCount - 1;

		foreach(Transform child in MiddlegroundParent)
		{
			child.localPosition = new Vector3 (0, MiddlegroundHeight * index, 0);
			index--;
		}

		index = ForegroundParent.childCount - 1;

		foreach(Transform child in ForegroundParent)
		{
			child.localPosition = new Vector3 (0, ForegroundHeight * index, 0);
			index--;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (transform.position.x != _oldPosition)
		{
			_positionDelta = _oldPosition - transform.position.x;

			_oldPosition = transform.position.x;
		}*/

		if(GameManager.Instance.GameState == GameState.Playing)
		{
			BackgroundParent.Translate (Vector3.down * BackgroundSpeed * Time.deltaTime);
			ForegroundParent.Translate (Vector3.down * ForegroundSpeed * Time.deltaTime);
			MiddlegroundParent.Translate (Vector3.down * MiddlegroundSpeed * Time.deltaTime);
		}
	}

	void CheckBackgroundPosition ()
	{
		foreach(Transform child in BackgroundParent)
		{
			if (child.position.y < _mainCamera.transform.position.y - BackgroundHeight)
			{
				Vector3 pos = BackgroundParent.GetChild (0).localPosition;
				pos.y += BackgroundHeight;
				child.localPosition = pos;

				child.SetSiblingIndex (0);
			}
		}

		foreach(Transform child in MiddlegroundParent)
		{
			if (child.position.y < _mainCamera.transform.position.y - MiddlegroundHeight)
			{
				Vector3 pos = MiddlegroundParent.GetChild (0).localPosition;
				pos.y += MiddlegroundHeight;
				child.localPosition = pos;

				child.SetSiblingIndex (0);
			}
		}

		foreach(Transform child in ForegroundParent)
		{
			if (child.position.y < _mainCamera.transform.position.y - ForegroundHeight)
			{
				Vector3 pos = ForegroundParent.GetChild (0).localPosition;
				pos.y += ForegroundHeight;
				child.localPosition = pos;

				child.SetSiblingIndex (0);
			}
		}
	}
}
