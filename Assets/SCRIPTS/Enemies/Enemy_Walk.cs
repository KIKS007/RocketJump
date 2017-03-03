using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Enemy_Walk : MonoBehaviour {

    bool goRight = true;
    Rigidbody rb;

    public float speed;

    Transform child;
    
    // Use this for initialization
	void Start () {
        child = transform.GetChild(0);
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {

        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);


        DetectBorder();
    
    }

    void DetectBorder()
    {
        RaycastHit hit;

        if (Physics.Raycast(child.transform.position, -Vector3.up, out hit))
        {


            if (hit.distance > 2)
            {
                ChangeDirection();
            }
        }

        if (Physics.Raycast(child.transform.position, transform.forward, out hit))
        {
            Debug.DrawRay(child.transform.position, transform.forward);

            if (hit.distance < 0.2)
            {
                ChangeDirection();
            }
        }

        
    }

    void ChangeDirection()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }



}
