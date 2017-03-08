using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;

public class Enemy_ForFlyer : MonoBehaviour 
{
	private bool _dead = false;

	public GameObject deathParticle;

	private string DeathSound = "SFX_EnemyDead";

	// Use this for initialization
	protected virtual void Start () 
	{
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		
	}

	protected virtual void OnCollisionEnter (Collision collision)
	{
		if (_dead)
			return;

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
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ScreenShakeCamera> ().CameraShaking (FeedbackType.Kill);
		VibrationManager.Instance.Vibrate (FeedbackType.Kill);

		Instantiate (deathParticle, transform.position, Quaternion.identity);

		MasterAudio.PlaySoundAndForget (DeathSound);

		_dead = true;

		ScoreManager.Instance.EnemyKilled (50);
		
		Animator animator = GetComponentInChildren<Animator>();
		animator.SetTrigger("Mort");

		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<Enemy_Fly>().speed = 0;

        StartCoroutine(Delaymort());
	}

    IEnumerator Delaymort ()
    {
		yield return new WaitForSecondsRealtime(0.1f);
        Destroy(gameObject);
        Debug.Log("Mort");
    }
}
