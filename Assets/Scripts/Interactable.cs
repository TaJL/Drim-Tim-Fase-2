using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
  public Light focusedLight;
  public Light unfocusedLight;
  public Transform target;

  Transform _slot;

  void OnEnable () {
    _slot = target.parent;
  }

  public void SetFocus (bool value) {
    focusedLight.enabled = value;
    unfocusedLight.enabled = !value;
  }

  public void Drop () {
    target.SetParent(_slot);
    Util.Reset(target);
  }
}
