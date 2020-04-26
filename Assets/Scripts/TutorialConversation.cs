using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TutorialConversation", menuName = "Scriptables/Conversation")]
public class TutorialConversation : ScriptableObject {
  public ConversationEntry[] entries;
}
