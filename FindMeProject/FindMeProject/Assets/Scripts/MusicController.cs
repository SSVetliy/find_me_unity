using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioSource audioSource;
    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    }

    void Update()
    {
        
    }
}
