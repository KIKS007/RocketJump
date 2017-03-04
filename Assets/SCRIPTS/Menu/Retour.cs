using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retour : MonoBehaviour {

    public GameObject PanelCredit;
    public GameObject PanelHowToplay;
  
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.Escape))
        {
            PanelCredit.SetActive(false);
            PanelHowToplay.SetActive(false);
        }
		
	}
}
