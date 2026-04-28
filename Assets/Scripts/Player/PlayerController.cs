using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float baseMoveSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 2f;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -20f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator animator;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip jumpSound;

    Rigidbody rb;
    Vector2 movement;
    LevelGenerator levelGenerator;

    float verticalVelocity = 0f;
    float groundY;
    bool isGrounded;
    bool jumpRequested;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
        groundY = rb.position.y;
    }

    void FixedUpdate()
    {
        HandleGroundCheck();
        HandleJumpVelocity();
        HandleMovement();
        HandleAnimations();
    }

    private void HandleGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = 0f;
    }

    private void HandleJumpVelocity()
    {
        if (!isGrounded)
            verticalVelocity += gravity * Time.fixedDeltaTime;
        else
            verticalVelocity = 0f;

        if (jumpRequested && isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpRequested = false;

            // Play jump sound
            if (audioSource != null && jumpSound != null)
                audioSource.PlayOneShot(jumpSound);
        }
    }

    private void HandleMovement()
    {
        float speedRatio = levelGenerator.CurrentMoveSpeed / levelGenerator.InitialMoveSpeed;
        float dynamicLateralSpeed = baseMoveSpeed * speedRatio;

        Vector3 currentPosition = rb.position;
        Vector3 moveDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 targetPosition = currentPosition + moveDirection * (dynamicLateralSpeed * Time.fixedDeltaTime);

        if (isGrounded && verticalVelocity <= 0f)
            targetPosition.y = currentPosition.y;
        else
            targetPosition.y = currentPosition.y + verticalVelocity * Time.fixedDeltaTime;

        // Never sink below starting ground level
        targetPosition.y = Mathf.Max(targetPosition.y, groundY);

        targetPosition.x = Mathf.Clamp(targetPosition.x, -xClamp, xClamp);
        targetPosition.z = Mathf.Clamp(targetPosition.z, -zClamp, zClamp);

        rb.MovePosition(targetPosition);
    }

    private void HandleAnimations()
    {
        animator.SetBool("IsGrounded", isGrounded);
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpRequested = true;
    }

    public void OnJumpButtonPressed()
    {
        jumpRequested = true;
    }
}