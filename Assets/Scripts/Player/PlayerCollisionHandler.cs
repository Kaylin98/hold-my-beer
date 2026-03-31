using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float collisionCooldown = 1f;

    const string JumpString = "Jump";
    const string vaultableString = "Vaultable";
    const string hitString = "Hit";

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
            case vaultableString:
                animator.SetTrigger(JumpString);
                break;
            default:
                animator.SetTrigger(hitString);
                break;
        }

        cooldownTimer = 0f;
    }
    
}
