using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] float increaseTimeOnCheckpoint = 5f;
    [SerializeField] float obstacleSpawnIntervalDecrease = 0.2f;
    [Header("Audio Settings")]
    [Tooltip("The big AI voice lines (Hold my ale, etc)")]
    [SerializeField] AudioClip[] voiceLines;
    
    [Tooltip("Short grunts, heavy breaths, or laughs for the fallback")]
    [SerializeField] AudioClip[] gruntSounds;
    
    [Tooltip("Percentage chance to play a FULL voice line instead of a grunt (e.g., 0.4 = 40%)")]
    [Range(0f, 1f)] 
    [SerializeField] float chanceToSpeak = 0.4f;

    // We use static memory so all checkpoints know what the last one played
    static int lastVoiceIndex = -1; 
    static int lastGruntIndex = -1;

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
            PlayerVoice playerVoice = other.GetComponentInChildren<PlayerVoice>();
            
            if (playerVoice != null)
            {
                // Roll the dice to see if we play a full voice line!
                if (Random.value <= chanceToSpeak && voiceLines.Length > 0)
                {
                    int randomIndex = Random.Range(0, voiceLines.Length);
                    
                    // Anti-repeat check for voices
                    if (randomIndex == lastVoiceIndex && voiceLines.Length > 1)
                    {
                        randomIndex = (randomIndex + 1) % voiceLines.Length; 
                    }

                    playerVoice.PlayVoiceLine(voiceLines[randomIndex]);
                    lastVoiceIndex = randomIndex;
                }
                // Failed the dice roll! Play a grunt instead.
                else if (gruntSounds.Length > 0)
                {
                    int randomIndex = Random.Range(0, gruntSounds.Length);
                    
                    // Anti-repeat check for grunts
                    if (randomIndex == lastGruntIndex && gruntSounds.Length > 1)
                    {
                        randomIndex = (randomIndex + 1) % gruntSounds.Length; 
                    }

                    playerVoice.PlayVoiceLine(gruntSounds[randomIndex]);
                    lastGruntIndex = randomIndex;
                }
            }

            gameManager.IncreaseTime(increaseTimeOnCheckpoint);
            obstacleSpawner.DecreaseObstacleSpwanInterval(obstacleSpawnIntervalDecrease);

        }
        
    }
}
