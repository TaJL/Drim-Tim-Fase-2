using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Glass : MonoBehaviour {
  public event System.Action onFilled;
  public SkinnedMeshRenderer liquid;
  public float drinkingSpeed = 10;
  public Vector2 drinkingIntervals = new Vector2(3, 8);

  Material _liquidMaterial;
  Coroutine _fill;

  void OnEnable () {
    _liquidMaterial = liquid.materials[0];
  }

  void Start () {
    StartCoroutine(_PassiveDrink());
  }

  public void FillWith (Color c) {
    _liquidMaterial.color = c;
    liquid.SetBlendShapeWeight(0, 0);
    if (_fill != null) {
      StopCoroutine(_fill);
    }
    _fill = StartCoroutine(_Fill());
  }

  IEnumerator _Fill () {
    float value;
    do {
      value = liquid.GetBlendShapeWeight(0);
      value -= Time.deltaTime * 200;
      value = Mathf.Clamp(value, 0, 100);
      liquid.SetBlendShapeWeight(0, value);
      yield return null;
    } while (value >= 1);

    _fill = null;
    if (onFilled != null) onFilled();
  }

  IEnumerator _PassiveDrink () {
    float elapsed = 0;
    float timeToDrink = Random.Range(drinkingIntervals.x, drinkingIntervals.y);
    bool drinking = Random.Range(0, 1f) < 0.5? true: false;
    if (drinking) timeToDrink *= 0.25f;

    while (true) {
      if (drinking) {
        float value = liquid.GetBlendShapeWeight(0) + Time.deltaTime * drinkingSpeed;
        value = Mathf.Clamp(value, 0, 100);

        liquid.SetBlendShapeWeight(0, value);
      }

      elapsed += Time.deltaTime;

      if (elapsed > timeToDrink) {
        elapsed = 0;
        drinking = !drinking;
        timeToDrink = Random.Range(drinkingIntervals.x, drinkingIntervals.y);
        if (drinking) timeToDrink *= 0.25f;
      }
      yield return null;
    }
  }
}
