using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VolumeSmoothDamp : MonoBehaviour {
  public float target = 0;
  public AudioSource speaker;
  public float smoothTime = 1;
  public float initial = 0;

  float _speed = 0;

  void Reset () {
    speaker = GetComponent<AudioSource>();
  }

  void OnEnable () {
    speaker.volume = initial < 0? speaker.volume: initial;
  }

  void Update () {
    speaker.volume = Mathf.SmoothDamp(speaker.volume, target,
                                      ref _speed, smoothTime);
  }
}
