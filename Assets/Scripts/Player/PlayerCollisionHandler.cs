using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator animator;

    [Header("Collision Settings")]
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float changeMoveSpeedAmount = -2f;

    const string JumpString = "Jump";
    const string vaultableString = "Vaultable";
    const string hitString = "Hit";

    float cooldownTimer = 0f;

    LevelGenerator levelGenerator;

    void Start() 
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < collisionCooldown) return;

        switch (collision.gameObject.tag)
        {
            case vaultableString:
                animator.SetTrigger(JumpString);
                break;
            default:
                animator.SetTrigger(hitString);
                break;
        }
        levelGenerator.ChangeChunkMoveSpeed(changeMoveSpeedAmount);
        cooldownTimer = 0f;
    }
    
}
