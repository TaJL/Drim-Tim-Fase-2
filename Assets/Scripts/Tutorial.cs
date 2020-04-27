using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour {
  public ClientsManager clients;
  public Client tutorialClient;
  public TutorialConversationManager manager;
  public TutorialConversation conversation;
  public TutorialConversation onClientRequest;
  public PowerButton rockola;

  void Awake () {
    clients.enabled = false;
  }

  IEnumerator Start () {
    yield return new WaitForSeconds(2);
    manager.Display(conversation);
    rockola.onPressed += SendTutorialCustomer;
  }

  public void SendTutorialCustomer () {
    rockola.onPressed -= SendTutorialCustomer;
    tutorialClient = clients.SpawnClient();
    tutorialClient.waiting_time = 999999;

    tutorialClient.OnRequest += HandleRequest;
    tutorialClient.OnClientEnded += HandleClientEnd;
  }

  public void HandleRequest (Recipe requested) {
    StartCoroutine(_TriggerClientTutorial());
  }

  public void HandleClientEnd (int score) {
    clients.enabled = true;
  }

  IEnumerator _TriggerClientTutorial () {
    yield return new WaitForSeconds(2);
    manager.Display(onClientRequest);
  }
}
