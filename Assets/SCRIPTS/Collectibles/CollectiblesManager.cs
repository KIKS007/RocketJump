using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : Singleton<CollectiblesManager> 
{
	public int CollectiblesCount;
	public int CollectiblesByChunk = 2;
	public int SpawnedCollectibles;

	[Header ("Spawn Limits")]
	public Vector2 XLimits;
	public Vector2 YLimits;
	public float Margin = 2;

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
		int index = 0;

		for(int j = 0; j < CollectiblesByChunk; j++)
		{
			GameObject collectibleGroup = null;
			Vector3 position = new Vector3 ();
			
			bool correctSpawn = true;
			
			do
			{
				collectibleGroup = AllCollectiblesGroup [Random.Range (0, AllCollectiblesGroup.Count)].gameObject;
				index++;

				for(int i = 0; i < 10; i++)
				{
					correctSpawn = true;

					float dividedHeight = (YLimits.y - YLimits.x) / CollectiblesByChunk;
					Vector2 modifedYRandom = new Vector2();
					modifedYRandom.x = YLimits.x + dividedHeight * j;
					modifedYRandom.y = YLimits.x + dividedHeight * (j + 1);

					position = new Vector3 (Random.Range (XLimits.x, XLimits.y), chunk.transform.position.y + Random.Range (modifedYRandom.x + Margin, modifedYRandom.y - Margin));

					foreach(Transform child in collectibleGroup.transform)
						if(Physics.CheckSphere (position + child.localPosition, CollectibleRadius, CollectibleMask, QueryTriggerInteraction.Ignore))
						{
							correctSpawn = false;
							break;
						}
				}
			}
			while (!correctSpawn || index < 5);


			Transform parent = chunk.transform.Find ("Collectibles Parent");
			Instantiate (collectibleGroup.gameObject, position, Quaternion.identity, parent);

			//Debug.Log (collectibleGroup.name + " in " + chunk.name);

			SpawnedCollectibles++;
		}
	}
}
