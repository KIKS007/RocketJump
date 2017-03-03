using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParallaxBackground : MonoBehaviour
{
	public ParallaxCamera parallaxCamera;
	List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

	[Header ("Settings")]
	public float BackgroundHeight;

	void Start()
	{
		if (parallaxCamera == null)
			parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
		if (parallaxCamera != null)
			parallaxCamera.onCameraTranslate += Move;

		GameManager.Instance.OnPlaying += SetLayers;

		SetLayers();
	}

	void SetLayers()
	{
		if (GameManager.Instance.GameState != GameState.Playing)
			return;

		parallaxLayers.Clear();
		for (int i = 0; i < transform.childCount; i++)
		{
			ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

			if (layer != null)
			{
				parallaxLayers.Add(layer);
				layer.transform.position = new Vector3 (layer.transform.position.x, BackgroundHeight * i, layer.transform.position.z);
			}
		}
	}
	void Move(float delta)
	{
		Transform highestLayer = parallaxLayers [0].transform;

		//Find Highest Layer
		foreach (ParallaxLayer layer in parallaxLayers)
			if (layer.transform.position.y > highestLayer.position.y)
				highestLayer = layer.transform;

		//Move Layers
		foreach (ParallaxLayer layer in parallaxLayers)
		{
			if(layer != null)
				layer.Move(delta);

			if(layer.transform.position.y < parallaxCamera.transform.position.y - BackgroundHeight)
				layer.transform.position = new Vector3 (layer.transform.position.x, highestLayer.position.y + (BackgroundHeight * (parallaxLayers.Count - 2)), layer.transform.position.z);
		}

		//Sort
		bool sorted = true;

		do
		{
			foreach (ParallaxLayer layer in parallaxLayers)
			{
				if (layer == null)
				{
					sorted = false;
					parallaxLayers.Remove (layer);
					break;
				}
			}
		}
		while (!sorted);
	}
}