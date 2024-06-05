using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] gameObjects;

    public GameObject field;
    public Transform playerPos;
    public int startMap = 3;

    private List<GameObject> mapParts = new List<GameObject>();
    private float spawnPos = 0;
    private float stepSpawn = 500;    

    private void Start()
    {
        for(int i = 0; i < startMap; i++)
        {
            SpawnMap();
        }
    }
    private void Update()
    {
        if(playerPos.position.z - 500 > spawnPos - (startMap * stepSpawn))
        {
            SpawnMap();
            DeleteMap();
        }
    }
    private void SpawnMap()
    {
        GameObject mapPart = Instantiate(field, transform.forward * spawnPos, Quaternion.identity);
        mapParts.Add(mapPart);
        spawnPos += stepSpawn;
    }
    private void DeleteMap()
    {
        Destroy(mapParts[0]);
        mapParts.RemoveAt(0);
    }
}
