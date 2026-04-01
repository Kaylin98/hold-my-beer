using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject rackPrefab;
    [SerializeField] GameObject beerPrefab;
    [SerializeField] GameObject coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] float beersSpawnChance = 0.3f; // Chance to spawn beer in a lane
    [SerializeField] float coinSpawnChance = 0.5f; // Chance to spawn coin in a lane
    [SerializeField] float coinSeperationLength = 2f; 
    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    
    List<int> availableLanes = new List<int> { 0, 1, 2 }; // Indices of available lanes

    void Start()
    {
        SpawnRacks();
        SpawnBeer();
        SpawnCoin();
    }

    void SpawnRacks()
    {
        int numberOfRacks = UnityEngine.Random.Range(0, lanes.Length); // Spawn between 1 and 3 racks

        for (int i = 0; i < numberOfRacks; i++)
        {
            if (availableLanes.Count <= 0) break; // No more lanes available

            int randomLaneIndex = SelectLane();

            Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
            Instantiate(rackPrefab, spawnPosition, Quaternion.identity, this.transform);
        }
    }
    void SpawnBeer()
    {
        if (UnityEngine.Random.value > beersSpawnChance || (availableLanes.Count <= 0)) return; // Skip spawning beer based on chance
        
        int randomLaneIndex = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
        Instantiate(beerPrefab, spawnPosition, Quaternion.identity, this.transform);
    }
    
    void SpawnCoin()
    {
        if (UnityEngine.Random.value > coinSpawnChance || (availableLanes.Count <= 0)) return; // Skip spawning coin based on chance

        int randomLaneIndex = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinToSpawn = UnityEngine.Random.Range(1, maxCoinsToSpawn);

        float topOfChunkZ = transform.position.z + (2f * coinSeperationLength);

        for (int i = 0; i < coinToSpawn; i++)
        {
            float coinZPosition = topOfChunkZ - (i * coinSeperationLength);
            Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, coinZPosition);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform);
        }

        
    }
    int SelectLane()
    {
        int randomLaneIndex = UnityEngine.Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex); // Remove the selected lane to avoid duplicates
        return selectedLane;
    }

    
}
