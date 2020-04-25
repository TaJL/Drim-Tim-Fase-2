using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Util {
  public static void Reset (Transform t) {
    t.localPosition = Vector3.zero;
    t.localScale = Vector3.one;
    t.localRotation = Quaternion.identity;
  }
}
