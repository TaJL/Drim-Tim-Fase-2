using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteracter : MonoBehaviour {
  public Interactable selected;
  public Transform target;
  public bool grabbing = false;

  void Update () {
    UpdateSelected();
    UpdateGrab();
  }

  void UpdateGrab () {
    if (!selected) return;

    if (Input.GetMouseButtonDown(0)) {
      selected.target.SetParent(target);
      Util.Reset(selected.target);
      grabbing = true;
    }

    if (Input.GetMouseButtonDown(1) && grabbing) {
      grabbing = false;
      selected.Drop();
    }
  }

  void UpdateSelected () {
    if (grabbing) return;

    Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("interactables"))) {
      Interactable thing = hit.collider.GetComponentInParent<Interactable>();

      if (!thing) return;

      if (selected != thing && selected != null) {
        selected.SetFocus(false);
      }

      if (selected != thing) {
        selected = thing;
        thing.SetFocus(true);
      }
    } else {
      if (selected) {
        selected.SetFocus(false);
        selected = null;
      }
    }
  }
}
