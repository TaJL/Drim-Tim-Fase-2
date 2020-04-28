using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarAmbience : MonoBehaviour {
  public int clientsInBar = 0;
  public AudioSource speaker;
  public float target = 0;

  float _speed = 0;

  void OnEnable () {
    Client.onAnyClientOrdered += (Client) => { clientsInBar++; UpdateSpeaker(); };
    Client.onAnyClientEnded += (Client) => { clientsInBar--; UpdateSpeaker(); };
  }

  void Update () {
    speaker.volume = Mathf.SmoothDamp(speaker.volume, target, ref _speed, 1);
  }

  void UpdateSpeaker () {
    target = Mathf.Lerp(0, 1, (clientsInBar - 2) / 3f);
  }
}
