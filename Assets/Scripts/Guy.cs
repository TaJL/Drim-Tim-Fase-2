using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour {
  public Animator animator;
  public Material[] skins;
  public Material[] shirts;
  public Material[] jeans;
  public Material[] hair;
  public Renderer r;
  public Vector2 heightRange = new Vector2(0.95f, 1.2f);

  void Start () {
    animator.SetFloat("offset", Random.Range(0,1f));

    Material[] materials = r.materials;
    materials[0] = skins[Random.Range(0, skins.Length)];
    materials[1] = shirts[Random.Range(0, skins.Length)];
    // materials[2] = shoes[Random.Range(0, skins.Length)];
    materials[3] = jeans[Random.Range(0, skins.Length)];
    materials[5] = hair[Random.Range(0, skins.Length)];
    r.materials = materials;

    transform.localScale = Vector3.one * Random.Range(heightRange.x, heightRange.y);
  }
}
