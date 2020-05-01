using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CreditsManager : MonoBehaviour {
  public bool quitted = false;
  public VolumeSmoothDamp volume;
  public Animator animator;

  void Update () {
    if (Input.GetKeyDown(KeyCode.Escape) && !quitted) {
      quitted = true;
      StartCoroutine(_Quit(() => { Application.Quit(); }));
    }

    if (Input.GetKeyDown(KeyCode.R) && !quitted) {
      quitted = true;
      StartCoroutine(_Quit(() => { SceneManager.LoadScene("SampleScene"); }));
    }
  }

  IEnumerator _Quit (System.Action action) {
    animator.SetTrigger("fade");
    volume.target = 0;
    volume.smoothTime = 0.8f;

    yield return new WaitForSeconds(1.5f);

    action();
  }
}
