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
  public VolumeSmoothDamp volume;
  public float initialVolume;

  Coroutine _fader;

    private Coroutine wait_routine;
    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        screen_text = GetComponentInChildren<TextMeshPro>();
        PlayClip(current_playing);
    }

  void Awake () {
    // initialVolume = audio_source.volume;
  }

  void OnEnable () {
    audio_source.volume = 0;
    volume.target = initialVolume;
    audio_source.Play();
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
                  audio_source.PlayOneShot(click);
                  PlayNext();
                }
                else if(hit.collider.CompareTag("Left"))
                {
                  audio_source.PlayOneShot(click);
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
        if(wait_routine != null)
            StopCoroutine(wait_routine);
        wait_routine = StartCoroutine(WaitUntilEnd());
    }

    private void SetText()
    {
      screen_text.text = string.Format("Now playing: {0}",clips[current_playing].name);
    }
    public void PlayNext()
    {
      if (_fader != null) StopCoroutine(_fader);
      current_playing = (current_playing + clips.Length + 1) % clips.Length;
      SetText();

      _fader = StartCoroutine(_FadeOutFadeIn(() => {
            PlayClip(current_playing);
          }));
    }
    public void PlayPrevious()
    {
      if (_fader != null) StopCoroutine(_fader);
      current_playing = (current_playing + clips.Length - 1) % clips.Length;
      SetText();

      _fader = StartCoroutine(_FadeOutFadeIn(() => {
            PlayClip(current_playing);
          }));
    }
    private IEnumerator WaitUntilEnd()
    {
      yield return new WaitUntil(delegate {
          return ((audio_source.clip.length - audio_source.time) <= volume.smoothTime * 2);
        });
      StartCoroutine(_FadeOutFadeIn(() => { PlayNext(); }));
      // yield return new WaitUntil(() => !audio_source.isPlaying);
      // PlayNext();
    }

  public void TogglePower ()
  {
    StopAllCoroutines();
    if (this.enabled) {
      this.enabled = false;
      audio_source.Stop();
      screen_text.text = "";
    } else {
        if(Events.OnRockolaPowered != null)
            Events.OnRockolaPowered();
      this.enabled = true;
      audio_source.Play();
      PlayClip(current_playing);
      SetText();
    }

    light.enabled = this.enabled;
    audio_source.PlayOneShot(click);
  }

  IEnumerator _FadeOutFadeIn (System.Action action = null) {
    volume.target = 0;
    yield return new WaitForSeconds(volume.smoothTime);
    if (action != null) {
      action();
    }
    volume.target = initialVolume;
    _fader = null;
  }
}
