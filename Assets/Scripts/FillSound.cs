using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FillSound : MonoBehaviour {
  public float targetVolume;
  public AudioSource speaker;

  float _speed = 0;

  void Update () {
    speaker.volume = Mathf.SmoothDamp(speaker.volume, targetVolume, ref _speed, 0.2f);
  }
}
