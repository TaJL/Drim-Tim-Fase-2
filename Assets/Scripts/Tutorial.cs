using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour {
  public ClientsManager clients;
  public Client tutorialClient;

  void Awake () {
    clients.enabled = false;
  }

  IEnumerator Start () {
    yield return new WaitForSeconds(0);
    tutorialClient = clients.SpawnClient();
    tutorialClient.waiting_time = 30;
  }
}
