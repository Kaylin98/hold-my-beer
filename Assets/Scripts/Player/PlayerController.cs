using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 2f;

    Rigidbody rb;
    Vector2 movement;
    LevelGenerator levelGenerator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Calculate the speed ratio (e.g., if world speed doubles, ratio is 2.0)
        float speedRatio = levelGenerator.CurrentMoveSpeed / levelGenerator.InitialMoveSpeed;
        
        // Multiply the player's base speed by that ratio
        float dynamicLateralSpeed = baseMoveSpeed * speedRatio;

        Vector3 currentPostion = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        
        // Use the new dynamic speed instead of the fixed amount
        Vector3 targetPosition = currentPostion + moveDirection * (dynamicLateralSpeed * Time.fixedDeltaTime);

        targetPosition.x = Mathf.Clamp(targetPosition.x, -xClamp, xClamp);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -zClamp, zClamp);

        rb.MovePosition(targetPosition);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}