using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        // If a music player already exists in the game, destroy this new duplicate
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; 
        }

        // If this is the first time the music player is created, keep it alive forever
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}