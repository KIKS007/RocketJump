using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaneChange : MonoBehaviour 
{
	public enum ChangeType { FirstToSecond, SecondToFirst, SecondToThird, ThirdToSecond};
	public ChangeType Change;

	private Vector3 _lanesPositions = new Vector3 (-14, 0, 14);
	private Ease _movementEase = Ease.OutQuad;
	private float _movementDuration = 0.5f;

	private float _velocityThreshold = 2;
	private Transform _camera;

	void Start ()
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}

	void OnTriggerEnter (Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			Rigidbody playerRigibody = collider.gameObject.GetComponent<Rigidbody> ();

			switch(Change)
			{
			case ChangeType.FirstToSecond:
				if(playerRigibody.velocity.x > _velocityThreshold)
					_camera.DOMoveX (_lanesPositions.y, _movementDuration).SetEase (_movementEase);
				break;

			case ChangeType.SecondToFirst:
				if(playerRigibody.velocity.x < -_velocityThreshold)
					_camera.DOMoveX (_lanesPositions.x, _movementDuration).SetEase (_movementEase);
				break;

			case ChangeType.SecondToThird:
				if(playerRigibody.velocity.x > _velocityThreshold)
					_camera.DOMoveX (_lanesPositions.z, _movementDuration).SetEase (_movementEase);
				break;

			case ChangeType.ThirdToSecond:
				if(playerRigibody.velocity.x < -_velocityThreshold)
					_camera.DOMoveX (_lanesPositions.y, _movementDuration).SetEase (_movementEase);
				break;
			}
		}
	}
}
