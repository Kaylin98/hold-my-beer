using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 10f;
    [Tooltip("How far away the rock needs to be before the shake drops to 0")]
    [SerializeField] float maxDistance = 40f; 
    [SerializeField] ParticleSystem rockHitEffect;
    [SerializeField] AudioSource rockHitSound;

    [SerializeField] float FXCooldown = 1f;
    float FXCooldownTimer = 0f;
    
    CinemachineImpulseSource impulseSource;

    void Awake() 
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update() 
    {
        FXCooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (FXCooldownTimer < FXCooldown) return;
        
        FireImpulse();
        CollisionFX(collision); 
        FXCooldownTimer = 0f;

    }

    void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        
        // If it's outside our max distance, ignore it completely
        if (distance > maxDistance) return;

        float shakeIntensity = (1f / distance) * shakeModifier;
        
        float distanceFactor = 1f - (distance / maxDistance);
        shakeIntensity = shakeIntensity * distanceFactor;

        shakeIntensity = Mathf.Min(shakeIntensity, 1f); 
        
        impulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contact = other.contacts[0];
        rockHitEffect.transform.position = contact.point;
        rockHitEffect.Play();
        rockHitSound.Play();
    }   
}