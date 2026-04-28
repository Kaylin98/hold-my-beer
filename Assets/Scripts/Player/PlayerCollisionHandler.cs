using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;
    
    // 1. Add a reference to the AudioSource component
    [SerializeField] AudioSource audioSource; 

    [Header("Audio Settings")]
    // 2. Add slots for your two different sounds
    [SerializeField] AudioClip vaultSound; 
    [SerializeField] AudioClip hitSound;  
    
    [Header("Collision Settings")]
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float changeMoveSpeedAmount = -2f;

    const string VaultString = "Vault";
    const string vaultableString = "Vaultable";
    const string hitString = "Hit";

    float cooldownTimer = 0f;

    LevelGenerator levelGenerator;
    GameManager gameManager;

    void Start() 
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameManager != null && gameManager.IsGameOver) return; 

        if (cooldownTimer < collisionCooldown) return;

        switch (collision.gameObject.tag)
        {
            case vaultableString:
                animator.SetTrigger(VaultString);
                PlayRandomizedSound(vaultSound); // Trigger Vault Audio
                break;
            default:
                animator.SetTrigger(hitString);
                PlayRandomizedSound(hitSound);  // Trigger Hit Audio
                break;
        }
        levelGenerator.ChangeChunkMoveSpeed(changeMoveSpeedAmount);
        cooldownTimer = 0f;
    }

    private void PlayRandomizedSound(AudioClip clipToPlay)
    {
        if (audioSource != null && clipToPlay != null)
        {
            // Shift the pitch up or down slightly
            audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
            
            // PlayOneShot plays the clip once all the way through
            audioSource.PlayOneShot(clipToPlay);
        }
    }
}