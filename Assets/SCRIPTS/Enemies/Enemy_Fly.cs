////using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Enemy_Fly : MonoBehaviour {

    Rigidbody rb;

	public LayerMask collisionLayer = (1 << 11) | (1 << 12) | (1 << 13);
    public float speed;
    public bool isRight = true;
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

        rb.MovePosition(transform.position + transform.right * -speed * Time.fixedDeltaTime);
        

        DetectBorder();
    
    }

    void DetectBorder()
    {
        RaycastHit hit;

         Debug.DrawRay(child.transform.position, Vector3.right * 2.7f, Color.red);

        Vector3 direction = isRight ? Vector3.right : Vector3.left;

        if (Physics.Raycast(child.transform.position, direction, out hit, 2.7f, collisionLayer))
        {
            Debug.Log("hit");
                isRight = !isRight;
                ChangeDirection();
            
        }        
    }

    void ChangeDirection()
    {
        transform.Rotate(new Vector3(0, 0, 0));
        speed = -speed;
    }



}
