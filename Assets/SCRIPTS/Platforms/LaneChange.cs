using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class LaneChange : MonoBehaviour 
{
	public enum ChangeType { Next, Previous };
	public ChangeType Change;

	public static LanePosition CurrentLane = LanePosition.Second;

	public static Vector3 LanesPositions = new Vector3 (-14, 0, 14);

	private Ease _movementEase = Ease.OutQuad;
	private float _movementDuration = 0.5f;

	private Transform _camera;

	private string NextLaneSound = "SFX_ChangeLane_LR";
	private string PreviousLaneSound = "SFX_ChangeLane_RL";

	public static event EventHandler OnLaneChange;

	void Start ()
	{
		_camera = GameObject.FindGameObjectWithTag ("MainCamera").transform;

		GameManager.Instance.OnPlaying += () => CurrentLane = LanePosition.Second;
	}

	void OnTriggerEnter (Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			if (Change == ChangeType.Next)
				Nextlane ();

			if (Change == ChangeType.Previous)
				PreviousLane ();
		}
	}

	void Nextlane ()
	{
		switch(CurrentLane)
		{
		case LanePosition.First:
			CurrentLane = LanePosition.Second;
			_camera.DOMoveX (LanesPositions.y, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		case LanePosition.Second:
			CurrentLane = LanePosition.Third;
			_camera.DOMoveX (LanesPositions.z, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		case LanePosition.Third:
			break;
		}

		if (OnLaneChange != null)
			OnLaneChange ();

		MasterAudio.PlaySoundAndForget (NextLaneSound);
	}

	void PreviousLane ()
	{
		switch(CurrentLane)
		{
		case LanePosition.First:
			break;
		case LanePosition.Second:
			CurrentLane = LanePosition.First;
			_camera.DOMoveX (LanesPositions.x, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		case LanePosition.Third:
			CurrentLane = LanePosition.Second;
			_camera.DOMoveX (LanesPositions.y, _movementDuration).SetEase (_movementEase).SetId ("LaneChange");
			break;
		}

		if (OnLaneChange != null)
			OnLaneChange ();
		
		MasterAudio.PlaySoundAndForget (PreviousLaneSound);
	}
}
