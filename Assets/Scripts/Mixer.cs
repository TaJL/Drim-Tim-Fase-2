using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mixer : MonoBehaviour {
  public SkinnedMeshRenderer skin;

  void Update () {
    if (Input.GetMouseButton(0) &&
        PlayerInteracter.Instance.grabbing) {
      float fillSpeed = PlayerInteracter.Instance.target
        .GetComponentInChildren<Bottle>().mixerFillSpeed;

      skin.SetBlendShapeWeight(0, skin.GetBlendShapeWeight(0) +
                               fillSpeed * Time.deltaTime);

    }
  }
}
