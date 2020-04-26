using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIConversationParticipant : MonoBehaviour {
  public Animator portrait;
  public Animator animator;
  public Text text;

  public IEnumerator _Say (string message) {
    portrait.SetBool("is talking", false);
    animator.SetBool("display", true);
    text.text = "";
    yield return new WaitForSeconds(1);

    portrait.SetBool("is talking", true);
    float elapsed = 0;
    float beginning = Time.time;
    string[] words = message.Split(' ');

    for (int i=0; i<words.Length; i++) {
      text.text += words[i] + " ";
      yield return new WaitForSeconds(0.05f);
    }

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
