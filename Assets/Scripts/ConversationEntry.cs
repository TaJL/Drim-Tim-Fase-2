using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ConversationEntry {
  [TextArea(5, 20)]
  public string message;
  public ConversationParticipant author;
}
