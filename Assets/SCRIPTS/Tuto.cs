using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour {

    public GameObject TutoCanvas;
	public Transform PreviousTrigger;


    string name;
    Transform text;
    GameObject triggerBox;
    GameObject player;
	Player playerScript;

    // Use this for initialization
    void Start () {
		playerScript = GetComponent<Player> ();
		player = GameObject.FindGameObjectWithTag("Player");
		playerScript.cantRocket = true;
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TriggerSloMoBox")
        {
            Camera.main.GetComponent<SlowMotion>().StartSlowMotion(0);
            triggerBox = other.gameObject;
            name = other.GetComponent<Collider>().name;

			PreviousTrigger = other.transform;

            if (name == "TriggerBox1")
            {
                text = TutoCanvas.transform.Find("TriggerBox1");
                text.gameObject.SetActive(true);
                text = TutoCanvas.transform.Find("TriggerBox2");
                text.gameObject.SetActive(false);
            }
            else if (name == "TriggerBox6")
            {
                text = TutoCanvas.transform.Find("TriggerBox6");
                text.gameObject.SetActive(false);
                text = TutoCanvas.transform.Find("TriggerBox7");
                text.gameObject.SetActive(true);
            }
            else
            {
                text = TutoCanvas.transform.Find(name);
                text.gameObject.SetActive(true);
            }

            playerScript.cantInput = true;
        }
    }

    public void DisableTutoText()
    {
        if (text.name == "TriggerBox2")
        {
                TutoCanvas.transform.Find("TriggerBox2").gameObject.SetActive(true);
                TutoCanvas.transform.Find("TriggerBox1").gameObject.SetActive(false);
                triggerBox.SetActive(true);
            
                text = TutoCanvas.transform.Find("TriggerBox1");

           
        }
        else if (text.name == "TriggerBox7")
        {
            //Debug.Log("PushButton7");
            TutoCanvas.transform.Find("TriggerBox7").gameObject.SetActive(false);
            TutoCanvas.transform.Find("TriggerBox6").gameObject.SetActive(true);
            triggerBox.SetActive(false);

			text = TutoCanvas.transform.Find("TriggerBox6");
			playerScript.cantRocket = false;
        }

        else
        {
            text.gameObject.SetActive(false);
            triggerBox.SetActive(false);
            Camera.main.GetComponent<SlowMotion>().StopSlowMotion();
            playerScript.cantInput = false;

			if(playerScript.gameObject.activeSelf)
           	 StartCoroutine("Wait");
        }
    }

    public void DisableTutoText2()
    {
		//Debug.Log("Tets");
		TutoCanvas.transform.Find("TriggerBox2").gameObject.SetActive(false);
		TutoCanvas.transform.Find("TriggerBox1").gameObject.SetActive(false);
		triggerBox.SetActive(false);
		TutoCanvas.transform.Find("TriggerBox6").gameObject.SetActive(false);
		text = TutoCanvas.transform.Find("TriggerBox1");
		
		
		
		if (text.name == "TriggerBox7")
		{
			TutoCanvas.transform.Find("TriggerBox7").gameObject.SetActive(false);
			triggerBox.SetActive(false);
			
			text = TutoCanvas.transform.Find("TriggerBox6");
			playerScript.cantRocket = false;
		}
		
		else
		{
			text.gameObject.SetActive(false);
			triggerBox.SetActive(false);
			Camera.main.GetComponent<SlowMotion>().StopSlowMotion();
			playerScript.cantInput = false;
			
			StartCoroutine("Wait");
        }


    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
    }

	public void End ()
	{
		GameManager.Instance.GameState = GameState.Menu;
		ScoreManager.Instance.ClearScore ();

		if (GameManager.Instance.FirstLaunch)
			GameManager.Instance.StartCoroutine ("LoadGame");
		else
		{
			UI.Instance.ShowMainMenu ();

			if(SceneManager.sceneCount > 1)
				for(int i = 1; i < SceneManager.sceneCount; i++)
					SceneManager.UnloadSceneAsync (SceneManager.GetSceneAt (i).name);
		}
	}

}
