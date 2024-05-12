using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] backgroundPrefabs;
    public Transform playerTransform;
    public float spawnZ = 0f; // spawn position
    public float tileLength = 10f; // length

    private float safeZone = 15f; 
    private int tilesOnScreen = 5;
    private int lastIndex = 0;

    private GameObject[] activeTiles;

    void Start()
    {
        activeTiles = new GameObject[tilesOnScreen];
        SpawnInitialTiles();
    }

    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnInitialTiles()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile(i);
        }
    }

    void SpawnTile(int tileIndex = -1)
    {
        GameObject tile;
        if (tileIndex == -1)
        {
            tile = Instantiate(backgroundPrefabs[Random.Range(0, backgroundPrefabs.Length)], transform.forward * spawnZ, Quaternion.identity);
        }
        else
        {
            tile = Instantiate(backgroundPrefabs[Random.Range(0, backgroundPrefabs.Length)], transform.forward * (spawnZ + tileIndex * tileLength), Quaternion.identity);
        }
        activeTiles[tileIndex] = tile;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[lastIndex]);
        lastIndex++;
        if (lastIndex >= tilesOnScreen)
        {
            lastIndex = 0;
        }
    }
}
