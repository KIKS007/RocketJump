using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMobManager : Singleton<CollectiblesManager> 
{
	public int MobCount;
	public int MobByChunk = 2;
	public int SpawnedMob;
    public GameObject FlyingMob;

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
        Debug.Log("TestSpawn");
		if (value > 0)
			MobCount += value;
		else
			Debug.LogWarning ("Negative Value!");
	}

	public void SpawnCollectibles (GameObject chunk)
	{
		for(int j = 0; j < MobCount; j++)
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
			Instantiate (FlyingMob, position, Quaternion.identity, parent);

			Debug.Log (collectibleGroup.name + " in " + chunk.name);

			SpawnedMob++;
		}
	}
}
