using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Chunk : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject rackPrefab;
    [SerializeField] GameObject beerPrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject winePrefab;

    [Header("Spawn Settings")]
    [SerializeField] float beersSpawnChance = 0.3f; // Chance to spawn beer in a lane
    [SerializeField] float coinSpawnChance = 0.5f; // Chance to spawn coin in a lane
    [SerializeField] float wineSpawnChance = 0.1f;
    [SerializeField] float coinSeperationLength = 2f; 
    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};

    List<int> availableLanes = new List<int> { 0, 1, 2 }; // Indices of available lanes
    LevelGenerator levelGenerator;
    ScoreManager scoreManager;
    MagnetController magnetController;

    void Start()
    {
        SpawnRacks();
        SpawnBeer();
        SpawnWine();
        SpawnCoin();
    }

    public void Init(LevelGenerator levelGenerator, ScoreManager scoreManager, MagnetController magnetController)
    {
        this.levelGenerator = levelGenerator;
        this.scoreManager = scoreManager;
        this.magnetController = magnetController;
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
        Beer newBeer = Instantiate(beerPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Beer>();
        newBeer.Init(levelGenerator);
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
            
            Coin newCoinGO =Instantiate(coinPrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Coin>();
            newCoinGO.Init(scoreManager, magnetController);
        }
    }

    void SpawnWine()
    {
        if (UnityEngine.Random.value > wineSpawnChance || (availableLanes.Count <= 0)) return; 

        int randomLaneIndex = SelectLane();

        Vector3 spawnPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z);
        Wine newWine = Instantiate(winePrefab, spawnPosition, Quaternion.identity, this.transform).GetComponent<Wine>();
        newWine.Init(magnetController);
    }

    int SelectLane()
    {
        int randomLaneIndex = UnityEngine.Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex); // Remove the selected lane to avoid duplicates
        return selectedLane;
    }

    
}
