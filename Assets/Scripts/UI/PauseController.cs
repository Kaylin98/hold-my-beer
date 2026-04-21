using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("UI Assignments")]
    [SerializeField] GameObject pauseMenuUI;

    // We make this static so the GameManager or ChunkMover can easily check if the game is paused!
    public static bool IsPaused = false; 

    void Start()
    {
        pauseMenuUI.SetActive(false);
        IsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // This completely freezes the game's physics and updates
        IsPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unfreeze the game
        IsPaused = false;
    }

    public void LoadMainMenu()
    {
        // THE TRAP: You MUST reset time scale before loading a new scene, 
        // otherwise your Main Menu animations will be frozen!
        Time.timeScale = 1f; 
        IsPaused = false;
        
        // Assuming your Main Menu is at Build Index 0
        SceneManager.LoadScene(0); 
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}