using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] AudioClip[] impactSounds;

    [Header("Collision Settings")]
    [SerializeField] string groundTag = "Ground"; 
    
    [Header("Bounce Dynamics")]
    [Tooltip("How hard it needs to hit to make a sound. Prevents machine-gun noise when resting.")]
    [SerializeField] float minImpactForce = 2f; 
    [Tooltip("The impact speed where the volume reaches 100%.")]
    [SerializeField] float maxImpactForce = 15f;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we hit the ground
        if (collision.gameObject.CompareTag(groundTag))
        {
            // Calculate exactly how hard the object smashed into the floor
            float hitForce = collision.relativeVelocity.magnitude;

            // Only play a sound if it hit harder than our minimum threshold
            if (hitForce > minImpactForce)
            {
                PlayRandomImpact(hitForce);
            }
        }
    }

    void PlayRandomImpact(float hitForce)
    {
        if (impactSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, impactSounds.Length);
            
            // Randomize pitch slightly
            audioSource.pitch = Random.Range(0.85f, 1.15f); 
            
            // Calculate the volume based on how hard it hit!
            // InverseLerp smoothly turns the force into a percentage between 0.0 and 1.0
            float dynamicVolume = Mathf.InverseLerp(minImpactForce, maxImpactForce, hitForce);
            
            // Play the sound at our newly calculated dynamic volume
            audioSource.PlayOneShot(impactSounds[randomIndex], dynamicVolume);
        }
    }
}