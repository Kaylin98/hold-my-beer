using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float obstacleSpawnInterval = 1f;
    int obstacleCount = 0;

    void Start() 
    {
        StartCoroutine(SpawnObstaclesRoutine());
    }

    IEnumerator SpawnObstaclesRoutine()
    {
        while (true)
        {
            Instantiate(obstaclePrefab, transform.position, Random.rotation);
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }
}
