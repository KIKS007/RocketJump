using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    public int numberOfEnemies = 1;

    public Transform enemiesParent;

    public List<GameObject> Enemies = new List<GameObject>();

    // Use this for initialization
    void Start () 
	{
		GameManager.Instance.OnGameOver += RemoveEnemies;
	}
	
	void RemoveEnemies ()
	{
		for (int i = 0; i < enemiesParent.childCount; i++)
			Destroy (enemiesParent.GetChild (i).gameObject);
	}
}
