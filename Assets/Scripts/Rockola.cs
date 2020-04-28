using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rockola : MonoBehaviour
{
    public AudioClip[] clips;
    private int current_playing = 0;
    public AudioSource audio_source;
    public TextMeshPro screen_text;
  public Light light;
  public AudioClip click;

    private Coroutine wait_routine;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        screen_text = GetComponentInChildren<TextMeshPro>();
        PlayClip(current_playing);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray =
                Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("3DButton")))
            {
                if (hit.collider.CompareTag("Right"))
                {
                    PlayNext();
                }
                else if(hit.collider.CompareTag("Left"))
                {
                    PlayPrevious();
                }
            }
        }
    }

    public void PlayClip(int index)
    {
        if(clips.Length == 0)
            return;
        
        audio_source.clip = clips[index];
        audio_source.Play();
        SetText();
        if(wait_routine != null)
            StopCoroutine(wait_routine);
        wait_routine = StartCoroutine(WaitUntilEnd());
    }

    private void SetText()
    {
        screen_text.text = string.Format("Now playing: {0}",audio_source.clip.name);
    }
    public void PlayNext()
    {
      audio_source.PlayOneShot(click);
        current_playing = (current_playing + clips.Length + 1) % clips.Length;
        PlayClip(current_playing);
    }
    public void PlayPrevious()
    {
      audio_source.PlayOneShot(click);
        current_playing = (current_playing + clips.Length - 1) % clips.Length;
        PlayClip(current_playing);
    }
    private IEnumerator WaitUntilEnd()
    {
        yield return new WaitUntil(delegate { return !audio_source.isPlaying; });
        PlayNext();
    }

  public void TogglePower () {
    StopAllCoroutines();
    if (this.enabled) {
      this.enabled = false;
      audio_source.Stop();
      screen_text.text = "";
    } else {
      this.enabled = true;
      audio_source.Play();
      PlayClip(current_playing);
    }

    light.enabled = this.enabled;
    audio_source.PlayOneShot(click);
  }
}
