using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    public int numberOfEnemies = 1;

    public Transform enemiesParent;

    public List<GameObject> Enemies = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
