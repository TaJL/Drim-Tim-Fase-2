using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mixer : MonoBehaviour {
  public SkinnedMeshRenderer skin;

  void Update () {
    RaycastHit hit;

    if (PlayerInteracter.Instance.grabbing &&
        Input.GetMouseButton(0) &&
        Physics.Raycast(PlayerInteracter.Instance.interactiveRay,
                        out hit, 10, LayerMask.GetMask("mixer"))) {

      Bottle bottle = PlayerInteracter.Instance.target
        .GetComponentInChildren<Bottle>();

      float value =
        Mathf.Clamp(skin.GetBlendShapeWeight(0) -
                    bottle.mixerFillSpeed * Time.deltaTime,
                    0, 100);

      skin.SetBlendShapeWeight(0, value);

    }
  }
}
