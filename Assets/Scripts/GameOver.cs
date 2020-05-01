using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour {
  IEnumerator Start () {
    yield return new WaitForSeconds(1.5f);
    SceneManager.LoadScene("Credits");
  }
}
