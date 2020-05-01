using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Vieja : MonoBehaviour {
  void Awake () {
    gameObject.SetActive(false);
    GameManager.onGameOver += GameOver;
  }

  void OnDestroy () {
    GameManager.onGameOver -= GameOver;
  }

  public void GameOver () {
    gameObject.SetActive(true);
  }
}
