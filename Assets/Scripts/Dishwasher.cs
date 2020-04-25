using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dishwasher : MonoBehaviour {
  void Update () {
    RaycastHit hit;

    if (Input.GetMouseButtonDown(0) &&
        PlayerInteracter.Instance.grabbing &&
        Physics.Raycast(PlayerInteracter.Instance.interactiveRay, out hit) &&
        hit.collider.GetComponentInParent<Dishwasher>()) {

      Mixer mixer = PlayerInteracter.Instance.selected.GetComponent<Mixer>();
      if (mixer) {
        mixer.Empty();
      }

    }
  }
}
