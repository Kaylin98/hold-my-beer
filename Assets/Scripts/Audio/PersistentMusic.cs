using UnityEngine;
using UnityEngine.SceneManagement; // Required to know what scene we are in

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        // Singleton logic (prevents duplicates when restarting the level)
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start listening for scene changes when this object is created
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Stop listening if this object gets destroyed (prevents memory leaks)
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") 
        {
            Destroy(gameObject);
        }
    }
}