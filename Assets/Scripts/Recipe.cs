using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Recipe", menuName = "Bar/Recipe")]
public class Recipe : ScriptableObject {
  public RequiredReagent[] reagents;
}
