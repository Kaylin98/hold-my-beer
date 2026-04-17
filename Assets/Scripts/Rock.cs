using UnityEngine;
using Unity.Cinemachine;

public class Rock : MonoBehaviour
{
    [SerializeField] float shakeModifier = 10f;
    [Tooltip("How far away the rock needs to be before the shake drops to 0")]
    [SerializeField] float maxDistance = 40f; 
    
    CinemachineImpulseSource impulseSource;

    void Awake() 
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void OnCollisionEnter(Collision collision)
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
}