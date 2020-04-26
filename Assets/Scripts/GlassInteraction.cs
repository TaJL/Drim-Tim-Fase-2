using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlassInteraction : MonoBehaviour {
  public event System.Action onEvaluated;
  public Glass glass;
  public float score;
  public Transform seat;
  public Client client;

  void Reset () {
    glass = GetComponent<Glass>();
  }

  void Start () {
    ClientsManager.onClientSpawned += HandleClient;
    gameObject.SetActive(false);
  }

  void OnDestroy () {
    ClientsManager.onClientSpawned -= HandleClient;
  }

  void Update () {
    RaycastHit hit;

    if (Input.GetMouseButtonDown(0) &&
        PlayerInteracter.Instance.grabbing &&
        Physics.Raycast(PlayerInteracter.Instance.interactiveRay, out hit) &&
        hit.collider.GetComponentInParent<GlassInteraction>()) {

      Mixer mixer = PlayerInteracter.Instance.selected.GetComponent<Mixer>();
      if (!mixer) return;

      mixer.Empty();
      glass.FillWith(mixer.material.color);
      // glass.onFilled += TriggerEvaluation;
      TriggerEvaluation();
    }
  }

  public void TriggerEvaluation () {
    // glass.onFilled -= TriggerEvaluation;
    Seat seat = GetComponentInParent<Bar>().GetSeatAt(this.seat.GetSiblingIndex());
    score = Mixer.Instance.Evaluate(seat.client.order);
  }


  public void HandleClient (Client client, int index) {
    if (index == seat.GetSiblingIndex()) {
      this.client = client;
      client.OnRequest += HandleRequest;
    }
  }

  public void HandleRequest (Recipe request) {
    gameObject.SetActive(true);
    client.OnRequest -= HandleRequest;
  }
}
