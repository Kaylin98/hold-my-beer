using UnityEngine;

public class Coin : Pickup
{
    ScoreManager scoreManager;
    MagnetController magnet;
    
    [Header("Coin Settings")]
    [SerializeField] int scoreAmount = 100;
    
    [Header("Magnet Settings")]
    [SerializeField] float magnetFlySpeed = 25f;

    public void Init(ScoreManager scoreManager, MagnetController magnet)
    {
        this.scoreManager = scoreManager;
        this.magnet = magnet;
    }

    void Update()
    {
        if (magnet != null && magnet.isMagnetActive)
        {
            float sqrDistance = (transform.position - magnet.transform.position).sqrMagnitude;
            float sqrRadius = magnet.magnetRadius * magnet.magnetRadius;

            if (sqrDistance <= sqrRadius)
            {
                transform.position = Vector3.MoveTowards(transform.position, magnet.transform.position, magnetFlySpeed * Time.deltaTime);
            }
        }
    }

    protected override void OnPickUp()
    {
        scoreManager.IncreaseScore(scoreAmount);
    }
}