using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalsPrefabs;
    private float spawnRangeX = 20;
    private float spawnPositionZ = 20;
    private float spawnDelay = 2;
    private float spawnInterval = 1.7f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", spawnDelay, spawnInterval);   
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void SpawnRandomAnimal()
    {
        int animalIndex = Random.Range(0, animalsPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPositionZ);

        Instantiate(animalsPrefabs[animalIndex],
            spawnPos,
            animalsPrefabs[animalIndex].transform.rotation);
    }
}
