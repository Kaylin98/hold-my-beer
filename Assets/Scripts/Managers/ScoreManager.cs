using System.Collections;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameManager gameManager;

    [Header("Magnet UI")]
    [Tooltip("The parent object holding both the Icon and the Text")]
    [SerializeField] GameObject magnetUIContainer; 
    [SerializeField] TextMeshProUGUI magnetTimerText;

    [Header("Heartbeat Animation")]
    [SerializeField] float pulseSpeed = 8f;
    [SerializeField] float minScale = 1.0f;
    [SerializeField] float maxScale = 1.25f;

    int score = 0;

    public void IncreaseScore(int amount)
    {
        if (gameManager.IsGameOver) return;
        
        score += amount;
        scoreText.text = $"{score}";
    }

    public void ToggleMagnetUI(bool isOn)
    {
        if (magnetTimerText != null)
        {
            magnetTimerText.gameObject.SetActive(isOn);

            if (isOn)
            {
                magnetTimerText.transform.localScale = Vector3.one;
            }
        }
    }
    
    public void UpdateMagnetTimer(float timeRemaining)
    {
        if (magnetTimerText != null)
        {
            magnetTimerText.text = timeRemaining.ToString("F1", CultureInfo.InvariantCulture) + "s";

            float wave = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f; 
            
            float currentScale = Mathf.Lerp(minScale, maxScale, wave);
            magnetTimerText.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }
        
    }
}
