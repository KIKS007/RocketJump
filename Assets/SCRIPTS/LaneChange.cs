using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaneChange : MonoBehaviour 
{
	public enum ChangeType { Next, Previous };
	public ChangeType Change;
	public int ThisLanePosition;

	private static int CurrentLane = 2;

	[HideInInspector]
	public static Vector3 _lanesPositions = new Vector3 (-14, 0, 14);

	private Ease _movementEase = Ease.OutQuad;
	private float _movementDuration = 0.5f;

	private float _velocityThreshold = 0;
	private Transform _camera;

	void Start ()
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		switch(transform.parent.parent.position.x.ToString ())
		{
		case "-14":
			ThisLanePosition = 1;
			break;
		case "0":
			ThisLanePosition = 2;
			break;
		case "14":
			ThisLanePosition = 3;
			break;
		}

		GameManager.Instance.OnPlaying += () => CurrentLane = 2;
	}

	void OnTriggerEnter (Collider collider)
	{

		if(collider.gameObject.tag == "Player")
		{
			Rigidbody playerRigibody = collider.gameObject.GetComponent<Rigidbody> ();

			if (Change == ChangeType.Next && playerRigibody.velocity.x > _velocityThreshold)
				Nextlane ();

			if (Change == ChangeType.Previous && playerRigibody.velocity.x < -_velocityThreshold)
				PreviousLane ();
		}
	}

	void Nextlane ()
	{
		switch(CurrentLane)
		{
		case 1:
			CurrentLane = 2;
			_camera.DOMoveX (_lanesPositions.y, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		case 2:
			CurrentLane = 3;
			_camera.DOMoveX (_lanesPositions.z, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		case 3:
			break;
		}
	}

	void PreviousLane ()
	{
		switch(CurrentLane)
		{
		case 1:
			break;
		case 2:
			CurrentLane = 1;
			_camera.DOMoveX (_lanesPositions.x, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		case 3:
			CurrentLane = 2;
			_camera.DOMoveX (_lanesPositions.y, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		}
	}
}
