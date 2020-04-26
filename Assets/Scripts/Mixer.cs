using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mixer : NonPersistantSingleton<Mixer> {
  public SkinnedMeshRenderer skin;
  public Dictionary<Reagent, float> content =
    new Dictionary<Reagent, float>();
  public Material material;
  public Recipe test;

  private bool is_pouring = false;


  void OnEnable () {
    material = skin.materials[0];
  }

  void Update () {
    RaycastHit hit;

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
          float value =
            Mathf.Clamp(skin.GetBlendShapeWeight(0) - delta, 0, 100);

          if (!content.ContainsKey(bottle.reagentData))
          {
            content[bottle.reagentData] = 0;
          }

          content[bottle.reagentData] += delta;

          skin.SetBlendShapeWeight(0, value);

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
    content.Clear();
    skin.SetBlendShapeWeight(0,100);
    material.color = new Color(1,1,1,1);
  }

  public float Evaluate (Recipe order) {
    print("EVALUATING! " + order + " " + order.NameOfDrink);
    float score = 0;

    Dictionary<string, RequiredReagent> hash =
      new Dictionary<string, RequiredReagent>();
    Dictionary<string, float> contentHash =
      new Dictionary<string, float>();

    foreach (KeyValuePair<Reagent, float> entry in content) {
      contentHash[entry.Key.reagentName] = entry.Value;
      print("this has: " + entry.Key.reagentName);
    }

    foreach (RequiredReagent reagent in order.reagents) {
      hash[reagent.reagentName] = reagent;
      if (!contentHash.ContainsKey(reagent.reagentName)) {
        print("it's missing " + reagent.reagentName);
        return 0;
      }
    }

    float failed = 0;
    foreach (KeyValuePair<Reagent, float> entry in content) {
      if (hash.ContainsKey(entry.Key.reagentName)) {
        float scored =
          Mathf.Max(0, 1 - Mathf.Abs((entry.Value / 20f) -
                                     hash[entry.Key.reagentName].amount));
        print("scored " + scored + " on " + entry.Key.reagentName +
              " it's required " + hash[entry.Key.reagentName].amount +
              " but you have " + (entry.Value / 20f));
        score += scored;
      } else {
        print("NOOOO WHY " + entry.Key.reagentName + "?!");
        failed += entry.Value / 20f;
      }
    }

    return failed > 0.1f? 0: ((score / order.reagents.Length) * 100);
  }


}
