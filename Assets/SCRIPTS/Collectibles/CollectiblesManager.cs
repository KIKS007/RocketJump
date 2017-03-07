﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : Singleton<CollectiblesManager> 
{
	public int CollectiblesCount;
	public int CollectibleByChunk = 2;
	public int SpawnedCollectibles;

	[Header ("Spawn Limits")]
	public Vector2 XLimits;
	public Vector2 YLimits;

	[Header ("Collectibles Prefabs")]
	public List<Transform> AllCollectiblesGroup = new List<Transform> ();

	[Header ("Settings")]
	public float CollectibleRadius = 0.45f;
	public LayerMask CollectibleMask;

	public void CollectiblePickedUp (int value = 1)
	{
		if (value > 0)
			CollectiblesCount += value;
		else
			Debug.LogWarning ("Negative Value!");
	}

	public void SpawnCollectibles (GameObject chunk)
	{
		for(int j = 0; j < CollectiblesCount; j++)
		{
			GameObject collectibleGroup = null;
			Vector3 position = new Vector3 ();
			
			bool correctSpawn = true;
			
			do
			{
				collectibleGroup = AllCollectiblesGroup [Random.Range (0, AllCollectiblesGroup.Count)].gameObject;
				
				for(int i = 0; i < 10; i++)
				{
					correctSpawn = true;
					
					position = new Vector3 (Random.Range (XLimits.x, XLimits.y), chunk.transform.position.y + Random.Range (YLimits.x, YLimits.y));

					foreach(Transform child in AllCollectiblesGroup)
						if(Physics.CheckSphere (child.position, CollectibleRadius, CollectibleMask, QueryTriggerInteraction.Ignore))
						{
							correctSpawn = false;
							break;
						}
					
					if(correctSpawn)
						break;
				}
			}
			while (!correctSpawn);


			Transform parent = chunk.transform.Find ("Collectibles Parent");
			Instantiate (collectibleGroup.gameObject, position, Quaternion.identity, parent);

			Debug.Log (collectibleGroup.name + " in " + chunk.name);

			SpawnedCollectibles++;
		}
	}
}
