using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    [SerializeField] TMP_Text scoreText;

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = $"{score}";
    }
}
