using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAtPlayer : MonoBehaviour {
  void Update () {
    transform.forward =
      Vector3.Scale(new Vector3(1,0,1),
                    transform.position - Camera.main.transform.position);
  }
}
