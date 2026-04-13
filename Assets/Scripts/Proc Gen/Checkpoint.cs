using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float increaseTimeOnCheckpoint = 5f;
    GameManager gameManager;
    private const string PlayerTag = "Player";


    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint reached!");

        if (other.CompareTag(PlayerTag))
        {
            gameManager.IncreaseTime(increaseTimeOnCheckpoint);
        }
        
    }
}
