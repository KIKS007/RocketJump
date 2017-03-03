using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	private static float _jumpForce = 10;
	private Rigidbody _playerRigidbody;


	// Use this for initialization
	protected virtual void Start () 
	{
		_playerRigidbody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody> ();	
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		
	}

	protected virtual void OnCollisionEnter (Collision collision)
	{
		
		if (LayerMask.NameToLayer ("Rocket") == collision.gameObject.layer)
			Death ();

        if (collision.gameObject.tag == "Player")
            HitByPlayer();  

    }

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (LayerMask.NameToLayer("Rocket") == collider.gameObject.layer)
            Death();
    }

    protected virtual void HitByPlayer ()
	{
        GameManager.Instance.GameOver();
	}

	protected virtual void Death ()
	{
		ScoreManager.Instance.EnemyKilled (50);
		
		Animator animator = GetComponentInChildren<Animator>();
		StartCoroutine(Delaymort());
		animator.SetTrigger("Mort");
	}

    IEnumerator Delaymort ()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Enemy_Walk>().speed = 0;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Destroy(gameObject);
    }
}
