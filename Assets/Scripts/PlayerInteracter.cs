using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteracter : NonPersistantSingleton<PlayerInteracter> {
  public Ray interactiveRay;
  public Interactable selected;
  public Transform target;
  public bool grabbing = false;
  [Header("Animation")] 
  public float grab_animation_time = 0.25f;
  public AnimationCurve grab_curve;
  private bool animating = false;
  public float pouring_time = 0.25f;
  public AnimationCurve pouring_curve;

  private void Start()
  {
    Events.OnStartPouring += StartPouring;
    Events.OnEndPouring += EndPouring;
  }

  void Update () {
    interactiveRay =
      Camera.main.ScreenPointToRay(new Vector2(Screen.width/2f, Screen.height/2f));

    UpdateSelected();
    UpdateGrab();
  }

  void UpdateGrab () {
    if (!selected) return;

    if (Input.GetMouseButtonDown(0) && !grabbing) {
      selected.target.SetParent(target);
      //Util.Reset(selected.target);
      StartCoroutine(AnimateTo(selected.target,target.localPosition));
      grabbing = true;
    }

    if (Input.GetMouseButtonDown(1) && grabbing) {
      selected.Drop();
      StartCoroutine(AnimateTo(selected.target, Vector3.zero));
      grabbing = false;
    }
  }

  void UpdateSelected () {
    if (grabbing) return;

    RaycastHit hit;

    if (Physics.Raycast(interactiveRay, out hit, 10, LayerMask.GetMask("interactables"))) {
      Interactable thing = hit.collider.GetComponentInParent<Interactable>();

      if (!thing) return;

      if (selected != thing && selected != null) {
        selected.SetFocus(false);
      }

      if (selected != thing) {
        selected = thing;
        thing.SetFocus(true);
      }
    } else {
      if (selected) {
        selected.SetFocus(false);
        selected = null;
      }
    }
  }

  private IEnumerator AnimateTo(Transform origin, Vector3 goal)
  {
    animating = true;
    var start_position = origin.localPosition;
    var end_position = goal;

    var start_rotation = origin.localRotation;
    var end_rotation = Quaternion.identity;
    var counter = 0.0f;
    while (counter < 1)
    {
      counter += Time.deltaTime/grab_animation_time ;
      origin.transform.localPosition = Vector3.LerpUnclamped(start_position, end_position, grab_curve.Evaluate(counter));
      origin.transform.localRotation = Quaternion.Lerp(start_rotation, end_rotation, grab_curve.Evaluate(counter));

      yield return null;
    }
    animating = false;
  }

  private void StartPouring()
  {
    var forward = Camera.main.transform.forward;
    forward.y = 0;
    var new_rotation = selected.target.localRotation * Quaternion.AngleAxis(-45, forward);
    selected.target.localRotation = Quaternion.identity;
    StartCoroutine(AnimatePour(selected.target,new_rotation));
  }

  private void EndPouring()
  {
    StartCoroutine(AnimatePour(selected.target,Quaternion.identity));
  }
  private IEnumerator AnimatePour(Transform origin, Quaternion end)
  {
    animating = true;
    var start_rotation = origin.localRotation;
    var end_rotation = end;
    var counter = 0.0f;
    while (counter < 1)
    {
      counter += Time.deltaTime/pouring_time;
      origin.localRotation = Quaternion.LerpUnclamped(start_rotation, end_rotation, pouring_curve.Evaluate(counter));

      yield return null;
    }
    animating = false;
  }
}
