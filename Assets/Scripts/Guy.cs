using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour {
  public Animator animator;
  public Material[] skins;
  public Material[] shirts;
  public Material[] jeans;
  public Material[] hair;
  public SkinnedMeshRenderer r;
  public Vector2 heightRange = new Vector2(0.95f, 1.2f);
  public int[] randomizable;
  public bool isTalking = false;
  public Client client;

  public float seed;
  public float speed = 10;

  void Start () {
    seed = Random.Range(0,1f);
    speed += Random.Range(0,5);
    animator.SetFloat("offset", Random.Range(0,1f));

    Material[] materials = r.materials;
    materials[0] = skins[Random.Range(0, skins.Length)];
    materials[1] = shirts[Random.Range(0, skins.Length)];
    // materials[2] = shoes[Random.Range(0, skins.Length)];
    materials[3] = jeans[Random.Range(0, skins.Length)];
    materials[5] = hair[Random.Range(0, skins.Length)];
    r.materials = materials;

    transform.localScale = Vector3.one * Random.Range(heightRange.x, heightRange.y);

    foreach (int i in randomizable) {
      r.SetBlendShapeWeight(i, (Random.Range(0, 4) / 3f) * 100);
    }

    client.onStartTalking += () => { isTalking = true; };
    client.onStopTalking += () => { isTalking = false; };
    client.onDisappointed += () => { r.SetBlendShapeWeight(0,100); };
    client.onPleased += () => { r.SetBlendShapeWeight(1,100); };
  }

  void Update () {
    if (isTalking) {
      r.SetBlendShapeWeight(2, Mathf.Abs(Mathf.Sin(seed + Time.time * speed)) * 100);
    } else {
      r.SetBlendShapeWeight(2, 0);
    }
  }

}
