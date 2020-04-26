using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialConversationManager : MonoBehaviour {
  public UIConversationParticipant professor;
  public UIConversationParticipant pupil;
  public const float LECTURE_TIME_PER_WORD = 0.4f;

  public TutorialConversation test;

  void Update () {
    if (Input.GetKeyDown(KeyCode.Q)) {
      Display(test);
    }
  }

  public void Display (TutorialConversation conversation) {
    StopAllCoroutines();
    StartCoroutine(_Display(conversation));
  }

  IEnumerator _Display (TutorialConversation conversation) {
    for (int i=0; i<conversation.entries.Length; i++) {
      print(i + ", " + conversation.entries[i]);
      if (conversation.entries[i].author == ConversationParticipant.Professor) {
        yield return StartCoroutine(professor._Say(conversation.entries[i].message));
      } else {
        yield return StartCoroutine(pupil._Say(conversation.entries[i].message));
      }
    }
  }
}
