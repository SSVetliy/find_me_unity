using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private List<AudioClip> audioClip;

    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void WinLevel()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip[0];
        audioSource?.Play();
    }

    public void LossLevel()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip[1];
        audioSource?.Play();
    }

    public void BuyItem()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip[2];
        audioSource?.Play();
    }

    public void FlipCard()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip[3];
        audioSource?.Play();
    }

    public void Error()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip[4];
        audioSource?.Play();
    }

    public void OpenChest()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip[5];
        audioSource?.Play();
    }

    void Update()
    {
        
    }
}
