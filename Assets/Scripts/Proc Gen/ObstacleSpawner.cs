using System.Collections;
using Unity.VisualScripting;
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
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(obstacleSpawnInterval);
            Instantiate(obstacleToSpawn, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
