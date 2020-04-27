using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Mixer : NonPersistantSingleton<Mixer> {
  public SkinnedMeshRenderer skin;

  public Dictionary<Reagent, float> content =
    new Dictionary<Reagent, float>(); // en "total del frasco"
  // el total del frasco es 100.
  // cada Reagent ocupa un porcentaje de éste 100.
  public Dictionary<string, float> simplifiedContent =
    new Dictionary<string, float>();

  public Material material;
  public Recipe test;
  public float defaultErrorAllowed = 0.5f; // en "unidades de medida" (va de 1 a 5)

  private bool is_pouring = false;


  void OnEnable () {
    material = skin.materials[0];
  }

  void Update () {

    RaycastHit hit;

    if (Input.GetKeyDown(KeyCode.P)) {
      print(FindCoincidence().NameOfDrink);
    }

    if (PlayerInteracter.Instance.grabbing &&
        PlayerInteracter.Instance.target.GetComponentInChildren<Bottle>() &&
        Physics.Raycast(PlayerInteracter.Instance.interactiveRay,
          out hit, 10, LayerMask.GetMask("mixer")))
    {
      if (Input.GetMouseButtonDown(0))
      {
        Events.OnStartPouring();
        is_pouring = true;
      }
      else if (Input.GetMouseButton(0))
      {
        if (PlayerInteracter.Instance.grabbing &&
            PlayerInteracter.Instance.target.GetComponentInChildren<Bottle>() &&
            Physics.Raycast(PlayerInteracter.Instance.interactiveRay,
              out hit, 10, LayerMask.GetMask("mixer")))
        {

          Bottle bottle = PlayerInteracter.Instance.target
            .GetComponentInChildren<Bottle>();

          float delta = bottle.mixerFillSpeed * Time.deltaTime;
          float value = skin.GetBlendShapeWeight(0) - delta;

          if (!content.ContainsKey(bottle.reagentData))
          {
            content[bottle.reagentData] = 0;
          }

          content[bottle.reagentData] += delta + (value < 0? value: 0);
          simplifiedContent[bottle.reagentData.reagentName] = content[bottle.reagentData];

          skin.SetBlendShapeWeight(0, Mathf.Clamp(value, 0, 100));

          material.color = new Color(0, 0, 0, 0);
          foreach (KeyValuePair<Reagent, float> entry in content)
          {
            material.color += entry.Key.liquidColor * (entry.Value / (100 - value));
          }
        }
      }
    }
    if (Input.GetMouseButtonUp(0))
    {
      if (is_pouring)
      {
        is_pouring = false;
        Events.OnEndPouring();
      }
    }
  }

  public void Empty () {
    simplifiedContent.Clear();
    content.Clear();
    skin.SetBlendShapeWeight(0,100);
    material.color = new Color(1,1,1,1);
  }

  public float Evaluate (Recipe order) {
    // print("EVALUATING! " + order + " " + order.NameOfDrink);
    float score = 0;

    Dictionary<string, RequiredReagent> hash =
      new Dictionary<string, RequiredReagent>();
    Dictionary<string, float> contentHash =
      new Dictionary<string, float>();

    foreach (KeyValuePair<Reagent, float> entry in content) {
      contentHash[entry.Key.reagentName] = entry.Value;
      // print("this has: " + entry.Key.reagentName);
    }

    foreach (RequiredReagent reagent in order.reagents) {
      hash[reagent.reagentName] = reagent;
      // if (!contentHash.ContainsKey(reagent.reagentName)) {
      //   // print("it's missing " + reagent.reagentName);
      //   return 0;
      // }
    }

    float failed = 0;
    foreach (KeyValuePair<Reagent, float> entry in content) {
      if (hash.ContainsKey(entry.Key.reagentName)) {
        float scored =
          Mathf.Max(0, 1 - Mathf.Abs((entry.Value / 20f) -
                                     hash[entry.Key.reagentName].amount));
        // print("scored " + scored + " on " + entry.Key.reagentName +
        //       " it's required " + hash[entry.Key.reagentName].amount +
        //       " but you have " + (entry.Value / 20f));
        score += scored;
      } else {
        // print("NOOOO WHY " + entry.Key.reagentName + "?!");
        failed += entry.Value / 20f;
      }
    }

    return failed > 0.1f? 0: ((score / order.reagents.Length) * 100);
  }

  public bool AccuracyComparation (Recipe recipe, float errorAllowedPerReagentRequired = -1) {
    float e = errorAllowedPerReagentRequired < 0? defaultErrorAllowed: errorAllowedPerReagentRequired;
    foreach (RequiredReagent required in recipe.reagents) {
      if (!simplifiedContent.ContainsKey(required.reagentName)) {
        return false;
      }
    }

    foreach (KeyValuePair<string, float> entry in simplifiedContent) {
      bool found = false;
      foreach (RequiredReagent required in recipe.reagents) {
        if (required.reagentName == entry.Key) {
          found = true;
          if (Mathf.Abs(required.amount - entry.Value / 20f) > e) {
            return false;
          }
          break;
        }
      }

      if (!found && entry.Value > errorAllowedPerReagentRequired) return false;
    }

    return true;
  }

  public List<string> WrongReagents(Recipe wanted)
  {
    var wrong_reagents = new List<string>();
    foreach (var current in simplifiedContent)
    {
      bool contains = false;
      foreach (var reagent in wanted.reagents)
      {
        if (reagent.reagentName == current.Key)
        {
          contains = true;
        }  
      }
      if(!contains)
        wrong_reagents.Add(current.Key);
    }
    return wrong_reagents;
  }
  public Recipe FindCoincidence (float errorAllowedPerReagentRequired = -1) {
    if (errorAllowedPerReagentRequired < 0) {
      errorAllowedPerReagentRequired = defaultErrorAllowed;
    }

    foreach (Recipe r in GameManager.all_recipes) {
      if (AccuracyComparation(r, errorAllowedPerReagentRequired)) return r;
    }

    return new Recipe();
  }
}
