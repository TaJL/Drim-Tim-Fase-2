using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerButton : MonoBehaviour {
  public event System.Action onPressed;

  void Update () {
    RaycastHit hit;
    if (Input.GetMouseButtonDown(0) &&
        Physics.Raycast(PlayerInteracter.Instance.interactiveRay,
                        out hit, 10 , LayerMask.GetMask("3DButton")) &&
                        hit.collider.GetComponent<PowerButton>() == this) {
      
      Rockola rockola = GetComponentInParent<Rockola>();
      rockola.TogglePower();
      if (onPressed != null) onPressed();
    }
  }
}
