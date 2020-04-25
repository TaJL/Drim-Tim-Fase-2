using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mixer : MonoBehaviour {
  public SkinnedMeshRenderer skin;
  public Dictionary<Reagent, float> content =
    new Dictionary<Reagent, float>();
  public Material material;

  void OnEnable () {
    material = skin.materials[0];
  }

  void Update () {
    RaycastHit hit;

    if (PlayerInteracter.Instance.grabbing &&
        Input.GetMouseButton(0) &&
        Physics.Raycast(PlayerInteracter.Instance.interactiveRay,
                        out hit, 10, LayerMask.GetMask("mixer"))) {

      Bottle bottle = PlayerInteracter.Instance.target
        .GetComponentInChildren<Bottle>();

      float delta = bottle.mixerFillSpeed * Time.deltaTime;
      float value =
        Mathf.Clamp(skin.GetBlendShapeWeight(0) - delta, 0, 100);

      if (!content.ContainsKey(bottle.reagentData)) {
        content[bottle.reagentData] = 0;
      }

      content[bottle.reagentData] += delta;

      skin.SetBlendShapeWeight(0, value);

      material.color = new Color(0,0,0,0);
      foreach (KeyValuePair<Reagent, float> entry in content) {
        material.color += entry.Key.liquidColor * (entry.Value / (100 - value));
      }
    }
  }
}
