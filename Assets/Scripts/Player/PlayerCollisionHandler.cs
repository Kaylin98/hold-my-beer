using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;

    float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < collisionCooldown) return;

        switch (collision.gameObject.tag)
        {
            case "Vaultable":
                animator.SetTrigger("Jump");
                break;
            default:
                animator.SetTrigger("Hit");
                break;
        }

        cooldownTimer = 0f;
    }
    
}
