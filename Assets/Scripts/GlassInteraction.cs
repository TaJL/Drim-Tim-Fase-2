using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlassInteraction : MonoBehaviour {
  public event System.Action onEvaluated;
  public Glass glass;
  public float score;
  public Transform seat;
  public Client client;
  public AudioClip shake;

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
        hit.collider.GetComponentInParent<GlassInteraction>() == this &&
        Mixer.Instance.LiquidAmount > 0.1f) {

      Mixer mixer = PlayerInteracter.Instance.selected.GetComponent<Mixer>();
      if (!mixer) return;

      TriggerEvaluation();
      glass.FillWith(mixer.material.color);
      mixer.Empty();
    }
  }

  public void TriggerEvaluation () {
    glass.onFilled -= TriggerEvaluation;
    Seat seat = GetComponentInParent<Bar>().GetSeatAt(this.seat.GetSiblingIndex());
    bool wasOk = Mixer.Instance.AccuracyComparation(seat.client.order);

    StopAllCoroutines();
    StartCoroutine(_PouringSound());
    seat.client.RateBeberage(wasOk, Mixer.Instance.WrongReagents(seat.client.order));
    score = wasOk? 100: 0;
    GameManager.score += (int) score;
  }

  IEnumerator _PouringSound () {
    GetComponentInChildren<AudioSource>().PlayOneShot(shake);
    yield return new WaitForSeconds(shake.length);
    GetComponent<FillSound>().targetVolume = 1;
    yield return new WaitForSeconds(0.5f);
    GetComponent<FillSound>().targetVolume = 0;
  }

  public void HandleClient (Client client, int index) {
    if (index == seat.GetSiblingIndex()) {
      this.client = client;
      client.OnRequest += HandleRequest;
      client.OnClientStandUp += HandleStandup;
    }
  }

  public void HandleRequest (Recipe request) {
    gameObject.SetActive(true);
    client.OnRequest -= HandleRequest;
  }

  public void HandleStandup () {
    client.OnClientStandUp -= HandleStandup;
    gameObject.SetActive(false);
  }
}
