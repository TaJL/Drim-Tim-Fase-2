using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorGameOver : MonoBehaviour {
  public HingeJoint door;
  public bool isLeft;

  void Reset () {
    door = GetComponent<HingeJoint>();
  }

  void OnEnable () {
    GameManager.onGameOver += GameOver;
  }

  public void GameOver () {
    JointSpring spring = door.spring;
    spring.targetPosition = 90 * (isLeft? -1: 1);
    door.spring = spring;
  }
}
