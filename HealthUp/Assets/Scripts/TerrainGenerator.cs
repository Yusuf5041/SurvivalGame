using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;

//*********************************************
//MonoBehaviour used from UnityEngine library
//*********************************************

public class TerrainGenerator : MonoBehaviour
{
    public float depth = 20;
    public int width;
    public int height;

    public float scale;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public GameObject[] rocks;
    public GameObject[] vegetation;

    public NavMeshSurface nms;

    public TerrainLayer terrainLayer;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
        Terrain terrain = gameObject.GetComponent<Terrain>();
        terrain.terrainData = TerrainGeneration(terrain.terrainData);
        Debug.Log("first script gen");
        BuildNav();
    }

    //generaete terrain data with new heights and objects
    private TerrainData TerrainGeneration(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenrateHeights());
        GenerateEnvironment();
        return terrainData;
    }

    //generate set of heights
    private float[,] GenrateHeights()
    {
        float[,] heights = new float[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    //calculate random heights with perlin noise 
    private float CalculateHeight(int x, int y)
    {
        float xCo = (float)x / width * scale + offsetX;
        float yCo = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCo, yCo);
    }

    void GenerateEnvironment()
    {
        int index = 0;

        int rockCount = Random.Range(200, 250);
        int treeCount = Random.Range(200, 250);

        for (int i = 0; i < rockCount; i++)
        {
            index = Random.Range(0, rocks.Length);
            RandomPlace(rocks[index]);
        }

        for (int i = 0; i < treeCount; i++)
        {
            index = Random.Range(0, vegetation.Length);
            RandomPlace(vegetation[index]);
        }

    }

    //randomly place object around terrain
    public void RandomPlace(GameObject objectToPlace)
    {
        int posx = Random.Range(1, 512);
        int posz = Random.Range(1, 512);
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));

        if (objectToPlace.tag == "Food")
            posy += 2.5f;

        GameObject placedObject = Instantiate(objectToPlace, new Vector3(posx, posy, posz), objectToPlace.transform.rotation);

        Vector3 randRotate = new Vector3(0, Random.Range(0, 360), 0);
        placedObject.transform.Rotate(randRotate);
    }

    void BuildNav()
    {
        nms.BuildNavMesh();
    }
}
