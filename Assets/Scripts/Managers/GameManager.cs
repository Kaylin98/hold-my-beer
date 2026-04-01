using System.Globalization;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] float startTime = 5f;

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

    private void DescreaseTime()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;
        timeText.text = timeRemaining.ToString("F1", CultureInfo.InvariantCulture);

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
