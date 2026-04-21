using UnityEngine;

public class LoadingSpinner : MonoBehaviour
{
    [SerializeField] float rotateSpeed = -250f; // Negative is clockwise

    void Update()
    {
        // Smoothly rotates the mug on the Z axis
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}