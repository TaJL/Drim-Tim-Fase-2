using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
  public Light focusedLight;
  public Light unfocusedLight;
  public Transform target;

  public AudioSource speaker;
  public AudioClip take;
  public AudioClip drop;

  public Transform _slot;

  void OnEnable () {
    _slot = target.parent;
  }

  public void SetFocus (bool value) {
    focusedLight.enabled = value;
    unfocusedLight.enabled = !value;
  }

  public void Drop () {
    print(Time.time);
    speaker.PlayOneShot(drop);
    target.SetParent(_slot);
    //Util.Reset(target);
  }

  public void Take () {
    print(Time.time);
    speaker.PlayOneShot(take);
  }
}
