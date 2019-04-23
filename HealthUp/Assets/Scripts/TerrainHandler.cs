using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainHandler : MonoBehaviour
{
    private GameObject newTerrain;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        newTerrain = Instantiate(gameObject.transform.parent.gameObject, gameObject.transform.parent.position + new Vector3(256, 0, 0), gameObject.gameObject.transform.parent.rotation);
        Terrain terrain = newTerrain.GetComponent<Terrain>();
        Terrain oldTerrain = gameObject.transform.parent.gameObject.GetComponent<Terrain>();
        terrain.terrainData = FixEdge(terrain, oldTerrain);

        Debug.Log("second script gen");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("out of box collider");
        Destroy(gameObject);
    }

    TerrainData FixEdge(Terrain newT, Terrain oldT)
    {
        TerrainGenerator tg = new TerrainGenerator();
        newT.terrainData.SetHeights(0, 0, FixHeights(newT.terrainData, oldT.terrainData));
        return newT.terrainData;
    }

    public float[,] FixHeights(TerrainData newT, TerrainData oldT)
    {
        float[,] newHeights = newT.GetHeights(0,0,256,256);
        float[,] oldHeights = oldT.GetHeights(0, 0, 256, 256);
        for (int y = 0; y < 256; y++)
        {
            newHeights[0, y] = oldHeights[256,y];
        }
        return newHeights;
    }
}
