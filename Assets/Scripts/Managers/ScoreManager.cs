using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameManager gameManager;
    int score = 0;

    public void IncreaseScore(int amount)
    {
        if (gameManager.IsGameOver) return;
        
        score += amount;
        scoreText.text = $"{score}";
    }
}
