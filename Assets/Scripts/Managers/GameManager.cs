using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] Animator playerAnimator;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    
    [Header("Sequence Timings")]
    [SerializeField] float startTime = 30f;
    [Tooltip("How many real seconds to show the slow-mo Game Over text")]
    [SerializeField] float slowMoDuration = 2f; 
    [Tooltip("Extra time to wait AFTER the voice line finishes before reloading")]
    [SerializeField] float postVoiceBuffer = 0.5f;

    [Header("Audio Settings")]
    [SerializeField] AudioSource voiceAudioSource; 
    [SerializeField] AudioClip[] gameOverVoiceLines;
    
    const string deathAnimationString = "GameOver";

    float timeRemaining;
    bool isGameOver = false;
    public bool IsGameOver => isGameOver;
    
    // We use this specific boolean to tell the chunks when to stop moving!
    public bool FreezeWorld { get; private set; } = false; 

    void Awake()
    {
    #if UNITY_ANDROID || UNITY_IOS
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = false;
    #endif
    }

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
        
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            StartCoroutine(GameOverSequence());
        }
        
        timeText.text = timeRemaining.ToString("F1", CultureInfo.InvariantCulture) + "s";
    }

    IEnumerator GameOverSequence()
    {
        // Lock the game and trigger SLOW MOTION with text
        isGameOver = true;
        playerController.enabled = false;
        gameOverText.SetActive(true);
        Time.timeScale = 0.1f;

        // Wait in real-time so the slow-mo doesn't delay this indefinitely
        yield return new WaitForSecondsRealtime(slowMoDuration);

        // Snap back to NORMAL SPEED, hide the text, and FREEZE the world
        Time.timeScale = 1f;
        gameOverText.SetActive(false);
        FreezeWorld = true; 

        float voiceLineLength = PlayRandomVoiceLineAndGetLength();

        // Fire the death animation!
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(deathAnimationString);
        }

       float finalWaitTime = (voiceLineLength > 0) ? voiceLineLength + postVoiceBuffer : 2.0f;

        yield return new WaitForSeconds(finalWaitTime);

        // Restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private float PlayRandomVoiceLineAndGetLength()
    {
        if (voiceAudioSource != null && gameOverVoiceLines.Length > 0)
        {
            int randomIndex = Random.Range(0, gameOverVoiceLines.Length);
            AudioClip chosenClip = gameOverVoiceLines[randomIndex];
            
            voiceAudioSource.PlayOneShot(chosenClip);
            
            return chosenClip.length;
        }

        return 2.5f; 
    }
}