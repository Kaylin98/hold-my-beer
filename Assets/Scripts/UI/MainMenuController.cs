using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI Panels")]
    [Tooltip("Drag your Loading Text or Panel here")]
    [SerializeField] GameObject loadingScreen; 

    public void PlayGame()
    {
        // Start loading the next scene in the background
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
        // 1. Turn on the Loading Screen UI so the player knows it's working
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // 2. Tell Unity to load the heavy scene data in the background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // 3. Keep the menu running smoothly while we wait for the load to finish
        while (!operation.isDone)
        {
            yield return null; // Wait for the next frame before checking again
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game!");
        Application.Quit();
    }
}