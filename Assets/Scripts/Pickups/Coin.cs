using UnityEngine;

public class Coin : Pickup
{
    ScoreManager scoreManager;
    [SerializeField]int scoreAmount = 100;
    
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    protected override void OnPickUp()
    {
        scoreManager.IncreaseScore(scoreAmount);
    }
}
