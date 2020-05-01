using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugMusic : MonoBehaviour {
  public Text text;
  public AudioSource src;

  void Update () {
    text.text = src.isPlaying + " " + src.volume + " active in hierarchy: " +
      src.gameObject.activeInHierarchy;
  }
}
