using System.Globalization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 60f;
    

    float timeRemaining;
    bool isGameOver = false;
    public bool IsGameOver => isGameOver;

    void Start()
    {
        timeRemaining = startTime;
    }

    void Update()
    {
        DescreaseTime();
    }

    public void IncreaseTime(float amount)
    {
        timeRemaining += amount;
    }

    private void DescreaseTime()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        timeText.text = timeRemaining.ToString("F1", CultureInfo.InvariantCulture) + "s";

        if (timeRemaining <= 0f)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
