using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                text.gameObject.SetActive(true);
            }
            else if (name == "TriggerBox6")
            {
                text = TutoCanvas.transform.Find("TriggerBox6");
                text.gameObject.SetActive(true);
                text = TutoCanvas.transform.Find("TriggerBox7");
                text.gameObject.SetActive(true);
            }
            else
            {
                text = TutoCanvas.transform.Find(name);
                text.gameObject.SetActive(true);
            }

            playerScript.cantInput = true;
            playerScript.cantRocket = true;
        }
    }

    public void DisableTutoText()
    {
        if (text.name == "TriggerBox2")
        {
            TutoCanvas.transform.Find("TriggerBox2").gameObject.SetActive(false);
            triggerBox.SetActive(false);

            text = TutoCanvas.transform.Find("TriggerBox1");


        }
        else if (text.name == "TriggerBox7")
        {
            TutoCanvas.transform.Find("TriggerBox7").gameObject.SetActive(false);
            triggerBox.SetActive(false);

            text = TutoCanvas.transform.Find("TriggerBox6");
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
        playerScript.cantRocket = false;
    }

}
