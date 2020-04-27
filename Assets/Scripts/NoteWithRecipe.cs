using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[ExecuteInEditMode]
public class NoteWithRecipe : MonoBehaviour {
  public Recipe data;
  public TextMeshPro label;

  void Reset () {
    label = GetComponentInChildren<TextMeshPro>();
  }

  void OnEnable () {
    UpdateText();
  }

  void Update () {
    if (Application.isPlaying) return;
    UpdateText();
  }

  public void UpdateText () {
    label.text = "<b>" + data.NameOfDrink + "</b>\n";
    name = data.NameOfDrink;

    for (int i=0; i<data.reagents.Length; i++) {
      label.text += data.reagents[i].amount + "x " + data.reagents[i].reagentName + "\n";
    }
  }
}
