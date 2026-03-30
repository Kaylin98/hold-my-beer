using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody rb;
    Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 currentPostion = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 targetPosition = currentPostion + moveDirection * (moveSpeed * Time.fixedDeltaTime);
        
        rb.MovePosition(targetPosition);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
