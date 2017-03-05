using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Enemy_Walk : MonoBehaviour {

    Rigidbody rb;

	public LayerMask collisionLayer = (1 << 11) | (1 << 12) | (1 << 13);
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

		if (Physics.Raycast(child.transform.position, -Vector3.up, out hit, collisionLayer))
        {
            if (hit.distance > 2)
            {
                ChangeDirection();
            }
        }

		if (Physics.Raycast(child.transform.position, transform.forward, out hit, collisionLayer))
        {
            Debug.DrawRay(child.transform.position, transform.forward);

            if (hit.distance < 0.5f)
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
