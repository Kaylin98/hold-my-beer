using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject tutorialOverlay;

    private const string TutorialKey = "HasSeenTouchTutorial";

    void Start()
    {
        int currentMode = PlayerPrefs.GetInt("SelectedControlType", 0);

        // Only show tutorial if Touch mode and first time playing
        if (currentMode == 1 && PlayerPrefs.GetInt(TutorialKey, 0) == 0)
        {
            tutorialOverlay.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
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