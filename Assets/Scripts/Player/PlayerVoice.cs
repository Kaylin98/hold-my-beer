using UnityEngine;

[RequireComponent(typeof(AudioSource))] // This guarantees the object will have an AudioSource
public class PlayerVoice : MonoBehaviour
{
    AudioSource voiceSource;

    void Awake()
    {
        voiceSource = GetComponent<AudioSource>();
    }

    public void PlayVoiceLine(AudioClip clip)
    {
        voiceSource.Stop(); 
        voiceSource.PlayOneShot(clip);
    }
}