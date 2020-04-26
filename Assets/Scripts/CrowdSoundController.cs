using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSoundController : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private AudioSource[] audioSources;
    [Range(0, 1)][SerializeField] private float audioVolume;
    private AudioSource audioActual;
    private float clipLenght;
    void Start()
    {
        audioSources[0].volume = audioVolume;
        audioSources[1].volume = 0;
        audioActual = audioSources[0];
        clipLenght = audioActual.clip.length;
        StartCoroutine(FadingCoroutine());
    }

    IEnumerator FadingCoroutine()
    {
        audioActual.Play();
        while (true)
        {   
            yield return new WaitForSeconds(clipLenght - fadeTime);

            AudioSource audioOld;

            if (audioActual == audioSources[0])
            {
                audioOld = audioSources[0];
                audioActual = audioSources[1];
            }
            else
            {
                audioOld = audioSources[1];
                audioActual = audioSources[0];
            }
            
            audioActual.Play();

            while (audioActual.volume < audioVolume)
                {
                    audioActual.volume += audioVolume/20;
                    audioOld.volume -= audioVolume/20;
                    yield return new WaitForSeconds(fadeTime/20);
                }
        }
    }
}
