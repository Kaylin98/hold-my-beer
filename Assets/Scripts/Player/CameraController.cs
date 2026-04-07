using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem speedLinesEffect;

    [Header("Camera Settings")]
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 5f;

    CinemachineCamera cinemachineCamera;

    void Awake() 
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float fovAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(fovAmount));

        if (fovAmount > 0)
        {
            speedLinesEffect.Play();
        }
        else
        {
            speedLinesEffect.Stop();
        }
    }

    IEnumerator ChangeFOVRoutine(float fovAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + fovAmount * zoomSpeedModifier, minFOV, maxFOV);

        float elapsedTime = 0f;
        
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
