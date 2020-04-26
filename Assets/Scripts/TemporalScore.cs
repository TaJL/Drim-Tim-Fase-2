using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TemporalScore : MonoBehaviour {
  public Text label;

  void Update () {
    label.text = GameManager.score + "";
  }
}
