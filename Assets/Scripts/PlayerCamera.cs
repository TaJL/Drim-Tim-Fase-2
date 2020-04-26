using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour {
  public float speedX;
  public float speedY;


  private float rotation_x = 0;
  private float rotation_y = 0;

  void Awake () {
    rotation_x = transform.rotation.eulerAngles.x;
    rotation_y = transform.rotation.eulerAngles.y;
  }

  void Update ()
  { 
      rotation_x += -speedY * Input.GetAxis("Mouse Y");
      rotation_x = Mathf.Clamp(rotation_x, -90, 90);
      rotation_y += speedX * Input.GetAxis("Mouse X");
      transform.rotation = Quaternion.Euler(rotation_x,rotation_y,0);
      // transform.Rotate(-speedY * Input.GetAxis("Mouse Y"),
      //                speedX * Input.GetAxis("Mouse X"),0);
    //Vector3 rotation = transform.rotation.eulerAngles;
    //rotation.z = 0;
    //transform.rotation = Quaternion.Euler(rotation);
  }
}
