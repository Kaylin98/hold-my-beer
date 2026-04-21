using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject tutorialOverlay;

    private const string TutorialKey = "HasSeenTouchTutorial";

    void Start()
    {
        if (PlayerPrefs.GetInt(TutorialKey, 0) == 0)
        {
            // It's their first time! Show the overlay and freeze the game.
            tutorialOverlay.SetActive(true);
            Time.timeScale = 0f; 
        }
        else
        {
            // They've played before. Hide the overlay and ensure time is running.
            tutorialOverlay.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void DismissTutorial()
    {
        PlayerPrefs.SetInt(TutorialKey, 1);
        PlayerPrefs.Save();
        
        tutorialOverlay.SetActive(false);
        Time.timeScale = 1f; 
    }

    [ContextMenu("Reset Tutorial Memory")]
    public void ResetTutorial()
    {
        PlayerPrefs.DeleteKey(TutorialKey);
        Debug.Log("Tutorial memory wiped. It will show up on next play.");
    }
}