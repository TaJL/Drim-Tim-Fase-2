using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseOcluder : MonoBehaviour {
  void Update () {
    if (Input.GetMouseButtonDown(0)) {
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
    }

    if (Input.GetKeyDown(KeyCode.Escape)) {
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
  }
}
