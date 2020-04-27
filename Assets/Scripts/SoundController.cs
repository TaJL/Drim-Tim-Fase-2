using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;

    public void PlayClip(int clipNumber, float volume)
    {
        audioSource.PlayOneShot(audioClips[clipNumber], volume);
    }
}
