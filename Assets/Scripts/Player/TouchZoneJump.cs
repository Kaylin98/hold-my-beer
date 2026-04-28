using UnityEngine;
using UnityEngine.EventSystems;

public class TouchZoneJump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] bool isLeftZone;

    static bool leftHeld = false;
    static bool rightHeld = false;
    static PlayerController playerController;

    void Awake()
    {
        if (playerController == null)
            playerController = FindFirstObjectByType<PlayerController>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isLeftZone) leftHeld = true;
        else rightHeld = true;

        if (leftHeld && rightHeld)
            playerController.OnJumpButtonPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isLeftZone) leftHeld = false;
        else rightHeld = false;
    }
}