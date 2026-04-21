using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlManager : MonoBehaviour
{
    [Header("Control UI Containers")]
    public GameObject joystickContainer;
    public GameObject touchSplitContainer;

    [Header("UI Button Setup")]
    public Button toggleButton;
    public TextMeshProUGUI buttonText;

    private const string ControlPrefKey = "SelectedControlType";
    private int currentMode;

    void Start()
    {
        // THE PLATFORM CHECK
        // If it's NOT a mobile device AND NOT the Unity Editor, hide the mobile UI and stop.
        if (!Application.isMobilePlatform && !Application.isEditor)
        {
            if (joystickContainer != null) joystickContainer.SetActive(false);
            if (touchSplitContainer != null) touchSplitContainer.SetActive(false);
            if (toggleButton != null) toggleButton.gameObject.SetActive(false);
            
            return; // This stops the rest of the script from running on PC
        }

        // THE MOBILE LOGIC (Runs only on phones or in the Simulator)
        currentMode = PlayerPrefs.GetInt(ControlPrefKey, 0); // Default to Joystick
        ApplyControls(currentMode);

        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(OnToggleClicked);
        }
    }

    void OnToggleClicked()
    {
        currentMode = (currentMode == 0) ? 1 : 0;
        ApplyControls(currentMode);
    }

    void ApplyControls(int mode)
    {
        if (mode == 0) // Joystick Mode
        {
            if (joystickContainer != null) joystickContainer.SetActive(true);
            if (touchSplitContainer != null) touchSplitContainer.SetActive(false);
            if (buttonText != null) buttonText.text = "CONTROLS: JOYSTICK";
            
            PlayerPrefs.SetInt(ControlPrefKey, 0);
        }
        else // Touch Mode
        {
            if (joystickContainer != null) joystickContainer.SetActive(false);
            if (touchSplitContainer != null) touchSplitContainer.SetActive(true);
            if (buttonText != null) buttonText.text = "CONTROLS: TOUCH";
            
            PlayerPrefs.SetInt(ControlPrefKey, 1);
        }

        PlayerPrefs.Save();
    }
}