using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour {
  public float speedX;
  public float speedY;

  void Update () {
    transform.Rotate(-speedY * Input.GetAxis("Mouse Y"),
                     speedX * Input.GetAxis("Mouse X"),0);
    Vector3 rotation = transform.rotation.eulerAngles;
    rotation.z = 0;
    transform.rotation = Quaternion.Euler(rotation);
  }
}
