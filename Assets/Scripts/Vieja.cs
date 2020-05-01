using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vieja : MonoBehaviour {
  void OnEnable () {
    gameObject.SetActive(false);
    GameManager.onGameOver += GameOver;
  }

  public void GameOver () {
    gameObject.SetActive(true);
  }
}
