using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{

    // Start is called before the first frame update
    void Start()
    {

        totalCount = 20;
        currentCount = totalCount;

        InitialSpawner();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnControl()
    {
        for (; ; )
        {
            if (currentCount < totalCount)
            {
                UpdateSpawner();
            }
            yield return new WaitForSeconds(4);
        }

    }
    //void InitialSpawner()
    //{
    //    for (int i = 0; i < enemyCount - 5; i++)
    //    {
    //        terrainGen.RandomPlace(chomper);
    //    }
    //    for (int i = 0; i < 5; i++)
    //    {
    //        terrainGen.RandomPlace(giant);
    //    }
    //}

    //void Spawner()
    //{
    //    terrainGen.RandomPlace(giant);
    //    terrainGen.RandomPlace(chomper);
    //    currentCount += 2;
    //}
}
