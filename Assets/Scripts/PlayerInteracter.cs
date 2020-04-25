using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteracter : MonoBehaviour {
  public Interactable selected;

  void Update () {
    Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("interactables"))) {
      Interactable thing = hit.collider.GetComponentInParent<Interactable>();
      if (!thing) return;

      if (selected != thing && selected != null) {
        selected.SetFocus(false);
      }

      selected = thing;
      thing.SetFocus(true);
    } else {
      if (selected) {
        selected.SetFocus(false);
        selected = null;
      }
    }
  }
}
