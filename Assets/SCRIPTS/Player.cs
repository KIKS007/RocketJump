using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public GameObject test;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Instantiate (test, mainCamera.ScreenToWorldPoint (Input.GetTouch (0).position) + mainCamera.transform.position, Quaternion.identity);
		}

		if(Input.GetMouseButtonDown (0))
		{
			Instantiate (test, mainCamera.ScreenToWorldPoint (Input.mousePosition) + mainCamera.transform.position, Quaternion.identity);
		}
	}
}
