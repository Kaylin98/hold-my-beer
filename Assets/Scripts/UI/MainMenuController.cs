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
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null; 
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game!");
        Application.Quit();
    }
}