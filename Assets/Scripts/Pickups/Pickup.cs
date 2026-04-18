using Unity.VisualScripting;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 120f;
    const string playerString = "Player";

    [Header("Audio Settings")]
    [Tooltip("The sound to play when this specific item is collected")]
    [SerializeField] AudioClip pickupSound;
    [SerializeField] [Range(0f, 1f)] float volume = 0.8f;

    protected virtual void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            if (pickupSound != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, volume);
            }

            OnPickUp();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickUp();
}