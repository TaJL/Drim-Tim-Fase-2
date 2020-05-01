using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerGameOver : MonoBehaviour {
  public Transform target;

  void Awake () {
    GameManager.onGameOver += GameOver;
    this.enabled = false;
  }

  void OnDestroy () {
    GameManager.onGameOver -= GameOver;
  }

  void FixedUpdate () {
    Camera.main.transform.forward =
      Vector3.RotateTowards(Camera.main.transform.forward,
                            target.position - Camera.main.transform.position,
                            12 * Time.deltaTime, 1);
  }

  public void GameOver () {
    GetComponent<PlayerCamera>().enabled = false;
    GetComponent<PlayerInteracter>().enabled = false;
    this.enabled = true;
  }
}
