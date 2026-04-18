using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;
    
    [Header("Spawn Settings")] 
    [SerializeField] float obstacleSpawnInterval = 1f;
    [SerializeField] float minObstacleSpawnInterval = .2f;
    [SerializeField] float spawnWidth = 4f;

    [Header("Physics Fix")]
    [Tooltip("How hard to shoot the obstacle at the floor so it bounces in time")]
    [SerializeField] float downwardSpawnForce = 15f; 

    void Start() 
    {
        StartCoroutine(SpawnObstaclesRoutine());
    }

    public void DecreaseObstacleSpwanInterval(float amount)
    {
        obstacleSpawnInterval -= amount;

        if (obstacleSpawnInterval < minObstacleSpawnInterval)
        {
            obstacleSpawnInterval = minObstacleSpawnInterval;
        }
    }

    IEnumerator SpawnObstaclesRoutine()
    {
        while (true)
        {
            GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            
            // FIX 1: Only add the random X offset. Do not add Y and Z again!
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth), 0, 0);
            
            yield return new WaitForSeconds(obstacleSpawnInterval);
            
            GameObject spawnedObstacle = Instantiate(obstacleToSpawn, spawnPosition, Random.rotation, obstacleParent);

            // FIX 2: Shoot it downwards so it hits the floor and bounces quickly
            Rigidbody rb = spawnedObstacle.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.down * downwardSpawnForce, ForceMode.Impulse);
            }
        }
    }
}