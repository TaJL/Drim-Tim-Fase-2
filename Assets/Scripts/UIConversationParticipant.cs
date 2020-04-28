using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class UIConversationParticipant : MonoBehaviour {
  public Animator portrait;
  public Animator animator;
  public TextMeshProUGUI text;

  public IEnumerator _Say (string message) {
    float elapsed = 0;
    bool jump = false;

    portrait.SetBool("is talking", false);
    animator.SetBool("display", true);
    text.text = "";
    while (elapsed < 1 && !jump) {
      elapsed += Time.deltaTime;
      if (Input.GetKeyDown(KeyCode.Space)) jump = true;
      yield return null;
    }

    jump = false;
    elapsed = 0;
    portrait.SetBool("is talking", true);
    float beginning = Time.time;
    string[] words = message.Split(' ');

    for (int i=0; i<words.Length && !jump; i++) {
      elapsed = 0;
      text.text += words[i] + " ";
      while (elapsed < 0.05 || Input.GetKeyDown(KeyCode.Space)) {
        yield return null;
        elapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space)) jump = true;
      }
    }
    text.text = message;

    elapsed = Time.time - beginning;
    float required = TutorialConversationManager.LECTURE_TIME_PER_WORD *
      (message.Split(' ').Length-1);

    while (elapsed < required && !Input.GetKeyDown(KeyCode.Space)) {
      yield return null;
      elapsed += Time.deltaTime;
      if (elapsed > required - 1) {
        portrait.SetBool("is talking", false);
      }
    }

    animator.SetBool("display", false);
  }
}
