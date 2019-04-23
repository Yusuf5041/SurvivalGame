using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : Spawner
{
    public bool newWave = false;
    // Start is called before the first frame update
    void Start()
    {

        totalCount = 50;
        currentCount = totalCount;

        InitialSpawner();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CustomSpawner()
    {
        for (int i = 0; i < 3; i++)
        {
            UpdateSpawner();
        }
    }

    IEnumerator SpawnControl()
    {
        for (; ; )
        {
            if (currentCount < totalCount && newWave)
            {
                newWave = false;
                CustomSpawner();

            }
            yield return new WaitForSeconds(5);
        }
    }

    //instatiate initial food objects on terrain
    //void GenerateInitialFood(TreePrototype[] tp)
    //{

    //    int index = 0;
    //    for (int i = 0; i < foodCount - 10; i++)
    //    {
    //        index = Random.Range(0, badFood.Length);
    //        RandomPlace(badFood[index]);
    //    }
    //    for (int j = 0; j < foodCount - 15; j++)
    //    {
    //        index = Random.Range(0, goodFood.Length);
    //        RandomPlace(goodFood[index]);
    //    }
    //}
}
