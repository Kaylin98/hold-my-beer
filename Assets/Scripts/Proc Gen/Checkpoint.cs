using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float increaseTimeOnCheckpoint = 5f;
    [SerializeField] float obstacleSpawnIntervalDecrease = 0.2f;
    [SerializeField] AudioSource audioSource; 

    GameManager gameManager;
    ObstacleSpawner obstacleSpawner;
    private const string PlayerTag = "Player";


    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        obstacleSpawner = FindFirstObjectByType<ObstacleSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint reached!");

        if (other.CompareTag(PlayerTag))
        {
            audioSource.Play();
            gameManager.IncreaseTime(increaseTimeOnCheckpoint);
            obstacleSpawner.DecreaseObstacleSpwanInterval(obstacleSpawnIntervalDecrease);

        }
        
    }
}
