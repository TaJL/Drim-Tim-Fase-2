using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseOcluder : MonoBehaviour {
  public bool showMouse = false;

  void Start () {
    if (showMouse) {
      Display();
    }

    #if !UNITY_EDITOR
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    #endif
  }

  void Update () {
    if (Input.GetMouseButtonDown(0) && !showMouse) {
      Hide();
    }

    if (Input.GetKeyDown(KeyCode.Escape)) {
      Display();
    }
  }

  public void Hide () {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  public void Display () {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }
}
