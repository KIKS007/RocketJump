using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScorePopup : MonoBehaviour 
{
	[Header ("Settings")]
	public float yPos = 400f;
	public float movementDuration = 4f;
	public float scaleDuration = 0.25f;

	private RectTransform rect;

	// Use this for initialization
	void Start () 
	{
		scaleDuration += Random.Range (-0.1f, 0.1f);
		movementDuration += Random.Range (-0.1f, 0.1f);

		rect = GetComponent<RectTransform> ();
		rect.DOScale (1.5f, scaleDuration).SetLoops (-1, LoopType.Yoyo);
	
		rect.DOAnchorPosY (yPos, movementDuration).SetRelative ().OnComplete (()=> 
			{
				rect.DOScale (0, 0.2f).OnComplete (()=> Destroy (transform.parent.gameObject));
			});
	}
}
