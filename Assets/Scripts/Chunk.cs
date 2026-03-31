using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject rackPrefab;
    [SerializeField] GameObject beerPrefab;
    [SerializeField] float beersSpawnChance = 0.3f; // Chance to spawn beer in a lane
    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    List<int> availableLanes = new List<int> { 0, 1, 2 }; // Indices of available lanes

    void Start()
    {
        SpawnRacks();
        SpawnBeer();
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
    int SelectLane()
    {
        int randomLaneIndex = UnityEngine.Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex); // Remove the selected lane to avoid duplicates
        return selectedLane;
    }

    
}
