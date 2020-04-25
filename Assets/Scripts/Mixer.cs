using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mixer : MonoBehaviour {
  public SkinnedMeshRenderer skin;

  void Update () {
    if (Input.GetMouseButton(0) &&
        PlayerInteracter.Instance.grabbing) {
      
    }
  }
}
