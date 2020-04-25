using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
  public Light focusedLight;
  public Light unfocusedLight;

  public void SetFocus (bool value) {
    focusedLight.enabled = value;
    unfocusedLight.enabled = !value;
  }
}
