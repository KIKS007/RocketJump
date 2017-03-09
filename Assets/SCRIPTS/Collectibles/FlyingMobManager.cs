using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMobManager : Singleton<FlyingMobManager> 
{
	public int MobCount;
	public int MobByChunk = 2;
	public int SpawnedMob;
    public GameObject FlyingMob;
    public Transform EnemiesParent;

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
        Debug.Log("TestSpawn");
		if (value > 0)
			MobCount += value;
		else
			Debug.LogWarning ("Negative Value!");
	}

	public void SpawnCollectibles (GameObject chunk)
	{
        for (int j = 0; j < MobByChunk; j++)
        {
            Vector3 position = new Vector3();
            bool correctSpawn = true;

            do
            {
                correctSpawn = true;

                float dividedHeight = (YLimits.y - YLimits.x) / MobByChunk;
                Vector2 modifedYRandom = new Vector2();
                modifedYRandom.x = YLimits.x + dividedHeight * j;
                modifedYRandom.y = YLimits.x + dividedHeight * (j + 1);

                position = new Vector3(Random.Range(XLimits.x, XLimits.y), chunk.transform.position.y + Random.Range(modifedYRandom.x + Margin, modifedYRandom.y - Margin));

                    if (Physics.CheckSphere(position, CollectibleRadius, CollectibleMask, QueryTriggerInteraction.Ignore))
                    {
                        correctSpawn = false;
                        break;
                    }
            }
            while (!correctSpawn);


           // Transform parent = chunk.transform.Find("Collectibles Parent");
            Instantiate(FlyingMob.gameObject, position, Quaternion.Euler(0,180,0), EnemiesParent);
            
            //Debug.Log (collectibleGroup.name + " in " + chunk.name);

            MobCount++;
        }
    }
	
}
