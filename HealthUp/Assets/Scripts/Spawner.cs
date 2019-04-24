using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class Spawner : MonoBehaviour
{
    public TerrainGenerator terrainGen;

    public int totalCount { get; set; }
    protected int currentCount { get; set; }

    public GameObject[] list1, list2;

    private float modifier;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void InitialSpawner()
    {
        int index = 0;
        for (int i = 0; i < totalCount - 5*modifier; i++)
        {
            index = Random.Range(0, list1.Length);
            terrainGen.RandomPlace(list1[index]);
        }
        for (int j = 0; j < totalCount - 5*modifier; j++)
        {
            index = Random.Range(0, list2.Length);
            terrainGen.RandomPlace(list2[index]);
        }
    }

    protected void UpdateSpawner()
    {
        terrainGen.RandomPlace(list1[Random.Range(0, list1.Length)]);
        terrainGen.RandomPlace(list2[Random.Range(0, list2.Length)]);
        currentCount += 3;
    }

    public void IncrementCount()
    {
        totalCount += 15;
    }

    public void DecrementCount()
    {
        currentCount--;
    }

    public void SetModifier(float mod)
    {
        modifier = mod;
    }
}
