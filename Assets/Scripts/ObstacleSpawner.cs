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
        while (obstacleCount < 5)
        {
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            obstacleCount++;
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }
}
