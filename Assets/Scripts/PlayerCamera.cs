using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour {
  public float speedX;
  public float speedY;
  public Transform camera;

  public float movementSpeed = 4;

  public Transform duck;
  public Transform normal;
  public Transform current;

  private float rotation_x = 0;
  private float rotation_y = 0;

  Vector3 _velocity;

  void Awake () {
    rotation_x = transform.localRotation.eulerAngles.x;
    rotation_y = transform.rotation.eulerAngles.y;
    current = normal;
    camera.position = normal.position;
  }

  void Update ()
  {
      rotation_x += -speedY * Input.GetAxis("Mouse Y");
      rotation_x = Mathf.Clamp(rotation_x, -90, 90);
      rotation_y += speedX * Input.GetAxis("Mouse X");
      camera.transform.localRotation = Quaternion.Euler(rotation_x,0,0);
      transform.rotation = Quaternion.Euler(0,rotation_y,0);
  }

  void FixedUpdate () {
      transform.Translate(movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime,
                          0, movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

      if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
        current = duck;
      } else {
        current = normal;
      }

      camera.transform.position =
        Vector3.SmoothDamp(camera.transform.position, current.position,
                           ref _velocity, 0.2f);

      // transform.Rotate(-speedY * Input.GetAxis("Mouse Y"),
      //                speedX * Input.GetAxis("Mouse X"),0);
    //Vector3 rotation = transform.rotation.eulerAngles;
    //rotation.z = 0;
    //transform.rotation = Quaternion.Euler(rotation);
  }
}
