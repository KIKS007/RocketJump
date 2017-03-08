using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuto : MonoBehaviour {

    public GameObject TutoCanvas;

    string name;
    Transform text;
    GameObject triggerBox;
    GameObject player;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(player.GetComponent<Player>().cantRocket);
	}
    
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "TriggerSloMoBox")
        {
            Camera.main.GetComponent<SlowMotion>().StartSlowMotion(0);
            triggerBox = other.gameObject;
            name = other.GetComponent<Collider>().name;

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

            player.GetComponent<Player>().cantInput = true;
            player.GetComponent<Player>().cantRocket = true;
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
            player.GetComponent<Player>().cantInput = false;

            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.05f);
        player.GetComponent<Player>().cantRocket = false;

    }

}
