using System.Collections;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    [Header("Magnet Settings")]
    public bool isMagnetActive = false;
    public float magnetRadius = 15f;
    public float magnetDuration = 10f;

    [Header("References")]
    [SerializeField] ScoreManager scoreManager;

    private float currentMagnetTime = 0f;

    public void ActivateMagnet()
    {
        currentMagnetTime = magnetDuration; 
        
        if (!isMagnetActive)
        {
            if (scoreManager != null) scoreManager.ToggleMagnetUI(true); 
            StartCoroutine(MagnetRoutine());
        }
    }

    IEnumerator MagnetRoutine()
    {
        isMagnetActive = true;

        while (currentMagnetTime > 0)
        {
            currentMagnetTime -= Time.deltaTime;
            
            if (scoreManager != null)
            {
                scoreManager.UpdateMagnetTimer(currentMagnetTime);
            }
            
            yield return null; 
        }

        isMagnetActive = false;
        
        if (scoreManager != null) scoreManager.ToggleMagnetUI(false); 
    }
}