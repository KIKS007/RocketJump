using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsaTuto : MonoBehaviour {

    public GameObject tutoP1;
    public GameObject tutoP2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowTutoP1 ()
    {
        tutoP1.SetActive(true);
    }

    public void HideTutoP1()
    {
        tutoP1.SetActive(false);
    }

    public void ShowTutoP2()
    {
        tutoP2.SetActive(true);
    }

    public void HideTutoP2()
    {
        tutoP2.SetActive(false);
        Time.timeScale = 1;
    }
}
